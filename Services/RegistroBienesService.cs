using ActivoFijo.Data;
using ActivoFijo.Dtos;
using ActivoFijo.Dtos.ActivoFijo.Dtos;
using ActivoFijo.Exceptions;
using ActivoFijo.Models;
using ActivoFijo.Repositories.IRepository;
using ActivoFijo.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ActivoFijo.Services
{
    public class RegistroBienesService : IRegistroBienesService
    {
        private readonly IRegistroBienesRepository _registroBienesRepository;
        private readonly IRegistroBienesTempRepository _registroBienesTempRepository;
        private readonly ExcelChunkReaderService _excelChunkReaderService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;


        public RegistroBienesService(
            IRegistroBienesRepository registroBienesRepository,
            IRegistroBienesTempRepository registroBienesTempRepository,
            ExcelChunkReaderService excelChunkReaderService,
            AppDbContext context,
            IMapper mapper,
            IUserContextService userContextService
        )

        {
            _registroBienesRepository = registroBienesRepository;
            _registroBienesTempRepository = registroBienesTempRepository;
            _excelChunkReaderService = excelChunkReaderService;
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;

        }


        public async Task<List<RegistroBienesTempDto>> CargarExcelAsync(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return new List<RegistroBienesTempDto>();
            }

            // Generar un identificador único para la carga
            var cargaId = $"{Path.GetFileNameWithoutExtension(archivo.FileName)}_{DateTime.Now:yyyyMMddHHmmss}";


            var mapeoColumnas = new List<(int Columna, Action<RegistroBienesTemp, string> AsignarValor)>
        {
            (0, (rb, valor) => rb.CodigoBien = valor),
            (1, (rb, valor) => rb.NombreBien = valor),
            (2, (rb, valor) => rb.FechaEfectos = DateTime.Parse(valor)),
            (3, (rb, valor) => rb.EstatusId = int.Parse(valor)),
            (4, (rb, valor) => rb.FotoBien = valor),
            (5, (rb, valor) => rb.Descripcion = valor),
            (6, (rb, valor) => rb.Marca = valor),
            (7, (rb, valor) => rb.Modelo = valor),
            (8, (rb, valor) => rb.Serie = valor),
            (9, (rb, valor) => rb.PartidaId = int.Parse(valor)),
            (10, (rb, valor) => rb.CambId = int.Parse(valor)),
            (11, (rb, valor) => rb.CucopId = int.Parse(valor)),
            (12, (rb, valor) => rb.NumeroContrato = valor),
            (13, (rb, valor) => rb.NumeroFactura = valor),
            (14, (rb, valor) => rb.FechaFactura = DateTime.Parse(valor)),
            (15, (rb, valor) => rb.ValorFactura = double.Parse(valor)),
            (16, (rb, valor) => rb.ValorDepreciado = double.Parse(valor)),
            (17, (rb, valor) => rb.UnidadAdministrativaId = int.Parse(valor)),
            (18, (rb, valor) => rb.UbicacionId = int.Parse(valor)),
            (19, (rb, valor) => rb.EmpleadoId = int.Parse(valor))

        };

            await _excelChunkReaderService.LeerArchivoExcelEnChunks(
                archivo,
                1000, // Tamaño del chunk
                mapeoColumnas,
                () => new RegistroBienesTemp { CargaId = cargaId },
                async (chunk) =>
                {
                    await _registroBienesTempRepository.AddRangeAsync(chunk);
                }
            );

            // Llamar al método para procesar los registros y obtener los registros inválidos
            var (registrosInvalidos, errorMessage) = await ProcesarRegistrosAsync(cargaId);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Manejar el error, por ejemplo, registrarlo o devolverlo al cliente
                Console.WriteLine(errorMessage);
                // Puedes lanzar una excepción o devolver una lista vacía, dependiendo de tu lógica de negocio
                throw new Exception(errorMessage);
            }

            var registrosInvalidosDto = _mapper.Map<List<RegistroBienesTempDto>>(registrosInvalidos);

            return registrosInvalidosDto;
        }

        public async Task<IEnumerable<RegistroBienesDto>> GetRegistroBienesByCriteria(int unidadAdministrativaId, int? estadoId = null, int? partidaId = null, int? estatusId = null, int? cambId = null, string? contrato = null, int? cucopId = null)
        {

            var registros = await _registroBienesRepository.GetRegistroBienesByCriteria(unidadAdministrativaId, estadoId, partidaId, estatusId, cambId, contrato, cucopId);

            if (registros == null || !registros.Any())
            {
                throw new ResourceNotFoundException("Registros de Bienes");
            }
            return _mapper.Map<IEnumerable<RegistroBienesDto>>(registros);

        }

        public async Task<List<RegistroBienesDto>> GetRegistroBienesByUnidadAdministrativaId(int unidadAdministrativaId)
        {
            var registros = await _registroBienesRepository.GetRegistroBienesByUnidadAdministrativaId(unidadAdministrativaId);

            if (registros == null || !registros.Any())
            {
                throw new ResourceNotFoundException("Registro de bienes", "Unidad Administrativa", unidadAdministrativaId);
            }

            return _mapper.Map<List<RegistroBienesDto>>(registros);
        }

        public async Task<(List<RegistroBienesTemp> registrosInvalidos, string errorMessage)> ProcesarRegistrosAsync(string cargaId)
        {
            var registrosInvalidos = new List<RegistroBienesTemp>();
            string errorMessage = null;

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "ProcesarRegistrosBienes";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var cargaIdParam = command.CreateParameter();
                        cargaIdParam.ParameterName = "@CargaId";
                        cargaIdParam.Value = cargaId;
                        command.Parameters.Add(cargaIdParam);

                        // Parámetro UserName
                        var userNameParam = command.CreateParameter();
                        userNameParam.ParameterName = "@UserName";
                        userNameParam.Value = _userContextService.GetUserName();
                        command.Parameters.Add(userNameParam);

                        // Parámetro IpAddress
                        var ipAddressParam = command.CreateParameter();
                        ipAddressParam.ParameterName = "@IpAddress";
                        ipAddressParam.Value = _userContextService.GetIpAddress();
                        command.Parameters.Add(ipAddressParam);

                        // Parámetro Fecha
                        var fechaParam = command.CreateParameter();
                        fechaParam.ParameterName = "@Fecha";
                        fechaParam.Value = _userContextService.GetRequestDate() ?? DateTime.UtcNow;
                        command.Parameters.Add(fechaParam);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    var registroInvalido = new RegistroBienesTemp
                                    {
                                        Id = reader.GetInt32(0),
                                        CodigoBien = reader.IsDBNull(1) ? null : reader.GetFieldValue<string>(1),
                                        NombreBien = reader.IsDBNull(2) ? null : reader.GetFieldValue<string>(2),
                                        FechaEfectos = reader.IsDBNull(3) ? (DateTime?)null : reader.GetFieldValue<DateTime>(3),
                                        EstatusId = reader.IsDBNull(4) ? (int?)null : reader.GetFieldValue<int>(4),
                                        FotoBien = reader.IsDBNull(5) ? null : reader.GetFieldValue<string>(5),
                                        Descripcion = reader.IsDBNull(6) ? null : reader.GetFieldValue<string>(6),
                                        Marca = reader.IsDBNull(7) ? null : reader.GetFieldValue<string>(7),
                                        Modelo = reader.IsDBNull(8) ? null : reader.GetFieldValue<string>(8),
                                        Serie = reader.IsDBNull(9) ? null : reader.GetFieldValue<string>(9),
                                        PartidaId = reader.IsDBNull(10) ? (int?)null : reader.GetFieldValue<int>(10),
                                        CambId = reader.IsDBNull(11) ? (int?)null : reader.GetFieldValue<int>(11),
                                        CucopId = reader.IsDBNull(12) ? (int?)null : reader.GetFieldValue<int>(12),
                                        NumeroContrato = reader.IsDBNull(13) ? null : reader.GetFieldValue<string>(13),
                                        NumeroFactura = reader.IsDBNull(14) ? null : reader.GetFieldValue<string>(14),
                                        FechaFactura = reader.IsDBNull(15) ? (DateTime?)null : reader.GetFieldValue<DateTime>(15),
                                        ValorFactura = reader.IsDBNull(16) ? (double?)null : reader.GetFieldValue<double>(16),
                                        ValorDepreciado = reader.IsDBNull(17) ? (double?)null : reader.GetFieldValue<double>(17),
                                        UnidadAdministrativaId = reader.IsDBNull(18) ? (int?)null : reader.GetFieldValue<int>(18),
                                        UbicacionId = reader.IsDBNull(19) ? (int?)null : reader.GetFieldValue<int>(19),
                                        EmpleadoId = reader.IsDBNull(20) ? (int?)null : reader.GetFieldValue<int>(20)

                                    };

                                    registrosInvalidos.Add(registroInvalido);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"ErrorNumber: {ex.HResult}, ErrorMessage: {ex.Message}";
            }

            return (registrosInvalidos, errorMessage);
        }

        public async Task<RegistroBienesDto> CreaRegistroBien(CreaRegistroBienesDto registroBienesDto)
        {
            var registroBienes = _mapper.Map<RegistroBienes>(registroBienesDto);

            registroBienes.UsuarioModifica = _userContextService.GetUserName();
            registroBienes.FechaModificacion = _userContextService.GetRequestDate() ?? DateTime.UtcNow;
            registroBienes.IPAddress = _userContextService.GetIpAddress();

            var nuevoRegistro = await _registroBienesRepository.CreateRegistroBienes(registroBienes);
            return _mapper.Map<RegistroBienesDto>(nuevoRegistro);
        }

        public async Task<RegistroBienesDto> ActualizaRegistroBien(ActualizaRegistroBienesDto registroBienesDto)
        {
            var registroBienes = await _registroBienesRepository.GetRegistroBienes(registroBienesDto.Id);
            if (registroBienes == null)
            {
                throw new ResourceNotFoundException("Registro de bienes", "ID", registroBienesDto.Id);
            }
            _mapper.Map(registroBienesDto, registroBienes);

            registroBienes.UsuarioModifica = _userContextService.GetUserName();
            registroBienes.FechaModificacion = _userContextService.GetRequestDate() ?? DateTime.UtcNow;
            registroBienes.IPAddress = _userContextService.GetIpAddress();

            var registroActualizado = await _registroBienesRepository.UpdateRegistroBienes(registroBienes);
            return _mapper.Map<RegistroBienesDto>(registroActualizado);
        }

        public async Task<RegistroBienesDto> EliminaRegistroBien(int registroBienesId)
        {
            var registroBienes = await _registroBienesRepository.GetRegistroBienes(registroBienesId);
            if (registroBienes == null)
            {
                throw new ResourceNotFoundException("Registro de bienes", "ID", registroBienesId);
            }
            registroBienes.UsuarioModifica = _userContextService.GetUserName();
            registroBienes.FechaModificacion = _userContextService.GetRequestDate() ?? DateTime.UtcNow;
            registroBienes.IPAddress = _userContextService.GetIpAddress();

            var registroEliminado = await _registroBienesRepository.DeleteRegistroBienes(registroBienesId);



            return _mapper.Map<RegistroBienesDto>(registroEliminado);
        }

        public async Task<RegistroBienesDto> GetRegistroBienes(int id)
        {
            var registroBienes = await _registroBienesRepository.GetRegistroBienes(id);
            if (registroBienes == null)
            {
                throw new ResourceNotFoundException("Registro de bienes", "ID", id);
            }
            return _mapper.Map<RegistroBienesDto>(registroBienes);



        }

        public async Task<IEnumerable<ContadorBienesPorUnidadDto>> ObtenerConteosPorUnidadAsync()
        {
            return await _registroBienesRepository.ObtenerConteosPorUnidadAsync();
        }

    }

}
