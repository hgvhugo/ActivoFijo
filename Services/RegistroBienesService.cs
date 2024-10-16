using ActivoFijo.Data;
using ActivoFijo.Dtos;
using ActivoFijo.Dtos.ActivoFijo.Dtos;
using ActivoFijo.Exceptions;
using ActivoFijo.Models;
using ActivoFijo.Repositories;
using ActivoFijo.Repositories.IRepository;
using ActivoFijo.Services.IServices;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace ActivoFijo.Services
{
    public class RegistroBienesService : IRegistroBienesService
    {
        private readonly IRegistroBienesRepository _registroBienesRepository;
        private readonly IRegistroBienesTempRepository _registroBienesTempRepository;
        private readonly IEmpleadoService _empleadoService;
        private readonly ExcelChunkReaderService _excelChunkReaderService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly PdfService _pdfService;
        private readonly ICorreoService _correoService;

        public RegistroBienesService(
            IRegistroBienesRepository registroBienesRepository,
            IRegistroBienesTempRepository registroBienesTempRepository,
            IEmpleadoService empleadoService,
            ExcelChunkReaderService excelChunkReaderService,
            AppDbContext context,
            IMapper mapper,
            IUserContextService userContextService,
            PdfService pdfService,
            ICorreoService correoService
        )

        {
            _registroBienesRepository = registroBienesRepository;
            _registroBienesTempRepository = registroBienesTempRepository;
            _empleadoService = empleadoService;
            _excelChunkReaderService = excelChunkReaderService;
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _pdfService = pdfService;
            _correoService = correoService;
        }
         
        public async Task<CargaExcelResult> CargarExcelResult(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                throw new BadRequestException("No se ha seleccionado un archivo");
            }
            if (archivo.Length > 10485760)
            {
                throw new BadRequestException("El archivo no puede ser mayor a 10MB");
            }

            if (await _registroBienesRepository.ExisteArchivoCargado(Path.GetFileName(archivo.FileName)))
            {
                throw new BadRequestException($"Ya existe una carga previamente realizada con archivo {Path.GetFileName(archivo.FileName)}, favor de validar");
            }


            // Generar un identificador único para la carga
            var cargaId = $"{Path.GetFileNameWithoutExtension(archivo.FileName)}_{DateTime.Now:yyyyMMddHHmmss}";


            var mapeoColumnas = new List<(int Columna, Action<RegistroBienesTemp, string> AsignarValor)>
        {
            (0, (rb, valor) => rb.CodigoBien = valor),
            (1, (rb, valor) => rb.NombreBien = valor),
            (2, (rb, valor) => rb.FechaEfectos = DateTime.Parse(valor)),
            (3, (rb, valor) => rb.EstatusId = int.Parse(valor)),
            //(4, (rb, valor) => rb.FotoBien = valor),
            (4, (rb, valor) => rb.Descripcion = valor),
            (5, (rb, valor) => rb.Marca = valor),
            (6, (rb, valor) => rb.Modelo = valor),
            (7, (rb, valor) => rb.Serie = valor),
            (8, (rb, valor) => rb.PartidaId = int.Parse(valor)),
            (9, (rb, valor) => rb.CambId = int.Parse(valor)),
            (10, (rb, valor) => rb.CucopId = int.Parse(valor)),
            (11, (rb, valor) => rb.NumeroContrato = valor),
            (12, (rb, valor) => rb.NumeroFactura = valor),
            (13, (rb, valor) => rb.FechaFactura = DateTime.Parse(valor)),
            (14, (rb, valor) => rb.ValorFactura = double.Parse(valor)),
            (15, (rb, valor) => rb.ValorDepreciado = double.Parse(valor)),
            (16, (rb, valor) => rb.UnidadAdministrativaId = int.Parse(valor)),
            (17, (rb, valor) => rb.UbicacionId = int.Parse(valor)),
            (18, (rb, valor) => rb.EmpleadoId = int.Parse(valor))

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


            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                var (errorMessage, archivoValidado) = await ValidaRegistrosAsync(cargaId, connection);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return new CargaExcelResult
                    {
                        ArchivoErrores = archivoValidado,
                        Mensaje = errorMessage
                    };
                }

                await ProcesarRegistrosBienesAsync(cargaId, Path.GetFileName(archivo.FileName), connection);
            }

            //// Llamar al método para procesar los registros y obtener los registros inválidos
            //var (errorMessage,archivoValidado) = await ValidaRegistrosAsync(cargaId);

            //if (!string.IsNullOrEmpty(errorMessage))
            //{
            //    // Retornar el mensaje de error y el archivo de errores
            //    return new CargaExcelResult
            //    {
            //        ArchivoErrores = archivoValidado,
            //        Mensaje = errorMessage
                  
            //    };

            //}

            //// Procesar los registros
            //await ProcesarRegistrosBienesAsync(cargaId, Path.GetFileName(archivo.FileName));



            return new CargaExcelResult
            {
                               
                Mensaje = "Carga exitosa."
            };
        }



        public async Task<(string errorMessage, byte[] excelFile)> ValidaRegistrosAsync(string cargaId, DbConnection connection)
        {
            var registrosInvalidos = new List<RegistroBienesTemp>();
            string errorMessage = null;
            byte[] excelFile = null;

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_ValidaCarga";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var cargaIdParam = command.CreateParameter();
                    cargaIdParam.ParameterName = "@CargaId";
                    cargaIdParam.Value = cargaId;
                    command.Parameters.Add(cargaIdParam);

                    var userNameParam = command.CreateParameter();
                    userNameParam.ParameterName = "@UserName";
                    userNameParam.Value = _userContextService.GetUserName();
                    command.Parameters.Add(userNameParam);

                    var ipAddressParam = command.CreateParameter();
                    ipAddressParam.ParameterName = "@IpAddress";
                    ipAddressParam.Value = _userContextService.GetIpAddress();
                    command.Parameters.Add(ipAddressParam);

                    var fechaParam = command.CreateParameter();
                    fechaParam.ParameterName = "@Fecha";
                    fechaParam.Value = _userContextService.GetRequestDate() ?? DateTime.Now;
                    command.Parameters.Add(fechaParam);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var registroInvalido = new RegistroBienesTemp
                                {
                                    CodigoBien = reader.IsDBNull(0) ? null : reader.GetFieldValue<string>(0),
                                    NombreBien = reader.IsDBNull(1) ? null : reader.GetFieldValue<string>(1),
                                    FechaEfectos = reader.IsDBNull(2) ? (DateTime?)null : reader.GetFieldValue<DateTime>(2),
                                    EstatusId = reader.IsDBNull(3) ? (int?)null : reader.GetFieldValue<int>(3),
                                    Descripcion = reader.IsDBNull(4) ? null : reader.GetFieldValue<string>(4),
                                    Marca = reader.IsDBNull(5) ? null : reader.GetFieldValue<string>(5),
                                    Modelo = reader.IsDBNull(6) ? null : reader.GetFieldValue<string>(6),
                                    Serie = reader.IsDBNull(7) ? null : reader.GetFieldValue<string>(7),
                                    PartidaId = reader.IsDBNull(8) ? (int?)null : reader.GetFieldValue<int>(8),
                                    CambId = reader.IsDBNull(9) ? (int?)null : reader.GetFieldValue<int>(9),
                                    CucopId = reader.IsDBNull(10) ? (int?)null : reader.GetFieldValue<int>(10),
                                    NumeroContrato = reader.IsDBNull(11) ? null : reader.GetFieldValue<string>(11),
                                    NumeroFactura = reader.IsDBNull(12) ? null : reader.GetFieldValue<string>(12),
                                    FechaFactura = reader.IsDBNull(13) ? (DateTime?)null : reader.GetFieldValue<DateTime>(13),
                                    ValorFactura = reader.IsDBNull(14) ? (double?)null : reader.GetFieldValue<double>(14),
                                    ValorDepreciado = reader.IsDBNull(15) ? (double?)null : reader.GetFieldValue<double>(15),
                                    UnidadAdministrativaId = reader.IsDBNull(16) ? (int?)null : reader.GetFieldValue<int>(16),
                                    UbicacionId = reader.IsDBNull(17) ? (int?)null : reader.GetFieldValue<int>(17),
                                    EmpleadoId = reader.IsDBNull(18) ? (int?)null : reader.GetFieldValue<int>(18),
                                    ErrorValidacion = reader.IsDBNull(19) ? null : reader.GetFieldValue<string>(19),
                                    CargaId = reader.IsDBNull(20) ? null : reader.GetFieldValue<string>(20),
                                };

                                registrosInvalidos.Add(registroInvalido);
                            }
                        }
                    }
                }

                if (registrosInvalidos.Any())
                {
                    errorMessage = "Se encontraron registros inválidos";

                    using (var workbook = new XSSFWorkbook())
                    {
                        var sheet = workbook.CreateSheet("Registros Invalidos");
                        var headerRow = sheet.CreateRow(0);

                        var properties = typeof(RegistroBienesTemp).GetProperties();
                        var columnasAIncluir = new List<string> { "CodigoBien", "NombreBien", "FechaEfectos", "EstatusId", "Descripcion", "Marca", "Modelo", "Serie", "PartidaId", "CambId", "CucopId", "NumeroContrato", "NumeroFactura", "FechaFactura", "ValorFactura", "ValorDepreciado", "UnidadAdministrativaId", "UbicacionId", "EmpleadoId", "ErrorValidacion", "CargaId" };

                        int columnIndex = 0;
                        var propertyIndexMap = new Dictionary<int, int>();

                        for (int i = 0; i < properties.Length; i++)
                        {
                            if (columnasAIncluir.Contains(properties[i].Name))
                            {
                                headerRow.CreateCell(columnIndex).SetCellValue(properties[i].Name);
                                propertyIndexMap[columnIndex] = i;
                                columnIndex++;
                            }
                        }

                        for (int i = 0; i < registrosInvalidos.Count; i++)
                        {
                            var row = sheet.CreateRow(i + 1);
                            foreach (var kvp in propertyIndexMap)
                            {
                                var value = properties[kvp.Value].GetValue(registrosInvalidos[i]);
                                row.CreateCell(kvp.Key).SetCellValue(value?.ToString());
                            }
                        }

                        using (var stream = new MemoryStream())
                        {
                            workbook.Write(stream);
                            excelFile = stream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"ErrorNumber: {ex.HResult}, ErrorMessage: {ex.Message}";
            }

            return (errorMessage, excelFile);
        }



        private async Task ProcesarRegistrosBienesAsync(string cargaId, string nombreArchivo, DbConnection connection)
        {
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_ProcesarRegistrosBienes";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var cargaIdParam = command.CreateParameter();
                    cargaIdParam.ParameterName = "@CargaId";
                    cargaIdParam.Value = cargaId;
                    command.Parameters.Add(cargaIdParam);

                    var nombreArchivoParam = command.CreateParameter();
                    nombreArchivoParam.ParameterName = "@NombreArchivo";
                    nombreArchivoParam.Value = nombreArchivo;
                    command.Parameters.Add(nombreArchivoParam);

                    var userNameParam = command.CreateParameter();
                    userNameParam.ParameterName = "@UserName";
                    userNameParam.Value = _userContextService.GetUserName();
                    command.Parameters.Add(userNameParam);

                    var ipAddressParam = command.CreateParameter();
                    ipAddressParam.ParameterName = "@IpAddress";
                    ipAddressParam.Value = _userContextService.GetIpAddress();
                    command.Parameters.Add(ipAddressParam);

                    var fechaParam = command.CreateParameter();
                    fechaParam.ParameterName = "@FECHA";
                    fechaParam.Value = _userContextService.GetRequestDate() ?? DateTime.Now;
                    command.Parameters.Add(fechaParam);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (DbException dbEx)
            {
                throw new Exception($"Error de base de datos al procesar los registros: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar los registros: {ex.Message}", ex);
            }
        }

        //public async Task<(string errorMessage, byte[] excelFile)> ValidaRegistrosAsync(string cargaId)
        //{
        //    var registrosInvalidos = new List<RegistroBienesTemp>();
        //    string errorMessage = null;
        //    byte[] excelFile = null;


        //    try
        //    {
        //        using (var connection = _context.Database.GetDbConnection())
        //        {
        //            await connection.OpenAsync();

        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = "sp_ValidaCarga";
        //                command.CommandType = System.Data.CommandType.StoredProcedure;

        //                var cargaIdParam = command.CreateParameter();
        //                cargaIdParam.ParameterName = "@CargaId";
        //                cargaIdParam.Value = cargaId;
        //                command.Parameters.Add(cargaIdParam);

        //                // Parámetro UserName
        //                var userNameParam = command.CreateParameter();
        //                userNameParam.ParameterName = "@UserName";
        //                userNameParam.Value = _userContextService.GetUserName();
        //                command.Parameters.Add(userNameParam);

        //                // Parámetro IpAddress
        //                var ipAddressParam = command.CreateParameter();
        //                ipAddressParam.ParameterName = "@IpAddress";
        //                ipAddressParam.Value = _userContextService.GetIpAddress();
        //                command.Parameters.Add(ipAddressParam);

        //                // Parámetro Fecha
        //                var fechaParam = command.CreateParameter();
        //                fechaParam.ParameterName = "@Fecha";
        //                fechaParam.Value = _userContextService.GetRequestDate() ?? DateTime.Now;
        //                command.Parameters.Add(fechaParam);

        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        while (await reader.ReadAsync())
        //                        {
        //                            var registroInvalido = new RegistroBienesTemp
        //                            {
        //                                //Id = reader.GetInt32(0),
        //                                CodigoBien = reader.IsDBNull(0) ? null : reader.GetFieldValue<string>(0),
        //                                NombreBien = reader.IsDBNull(1) ? null : reader.GetFieldValue<string>(1),
        //                                FechaEfectos = reader.IsDBNull(2) ? (DateTime?)null : reader.GetFieldValue<DateTime>(2),
        //                                EstatusId = reader.IsDBNull(3) ? (int?)null : reader.GetFieldValue<int>(3),
        //                                //FotoBien = reader.IsDBNull(5) ? null : reader.GetFieldValue<string>(5),
        //                                Descripcion = reader.IsDBNull(4) ? null : reader.GetFieldValue<string>(4),
        //                                Marca = reader.IsDBNull(5) ? null : reader.GetFieldValue<string>(5),
        //                                Modelo = reader.IsDBNull(6) ? null : reader.GetFieldValue<string>(6),
        //                                Serie = reader.IsDBNull(7) ? null : reader.GetFieldValue<string>(7),
        //                                PartidaId = reader.IsDBNull(8) ? (int?)null : reader.GetFieldValue<int>(8),
        //                                CambId = reader.IsDBNull(9) ? (int?)null : reader.GetFieldValue<int>(9),
        //                                CucopId = reader.IsDBNull(10) ? (int?)null : reader.GetFieldValue<int>(10),
        //                                NumeroContrato = reader.IsDBNull(11) ? null : reader.GetFieldValue<string>(11),
        //                                NumeroFactura = reader.IsDBNull(12) ? null : reader.GetFieldValue<string>(12),
        //                                FechaFactura = reader.IsDBNull(13) ? (DateTime?)null : reader.GetFieldValue<DateTime>(13),
        //                                ValorFactura = reader.IsDBNull(14) ? (double?)null : reader.GetFieldValue<double>(14),
        //                                ValorDepreciado = reader.IsDBNull(15) ? (double?)null : reader.GetFieldValue<double>(15),
        //                                UnidadAdministrativaId = reader.IsDBNull(16) ? (int?)null : reader.GetFieldValue<int>(16),
        //                                UbicacionId = reader.IsDBNull(17) ? (int?)null : reader.GetFieldValue<int>(17),
        //                                EmpleadoId = reader.IsDBNull(18) ? (int?)null : reader.GetFieldValue<int>(18),
        //                                //Procesado= reader.IsDBNull(19) ? (bool?)null :  reader.GetFieldValue<bool>(19),
        //                                ErrorValidacion = reader.IsDBNull(19) ? null : reader.GetFieldValue<string>(19),
        //                                CargaId= reader.IsDBNull(20) ? null : reader.GetFieldValue<string>(20),

        //                            };

        //                            registrosInvalidos.Add(registroInvalido);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        if (registrosInvalidos.Any())
        //        {
        //            errorMessage = "Se encontraron registros inválidos";

        //            //using (var workbook = new XSSFWorkbook())
        //            //{
        //            //    var sheet = workbook.CreateSheet("Registros Invalidos");
        //            //    var headerRow = sheet.CreateRow(0);

        //            //    // Crear encabezados
        //            //    var properties = typeof(RegistroBienesTemp).GetProperties();
        //            //    var columnasAIncluir = new List<string> { "CodigoBien", "NombreBien", "FechaEfectos", "EstatusId", "Descripcion", "Marca", "Modelo", "Serie", "PartidaId", "CambId", "CucopId", "NumeroContrato", "NumeroFactura", "FechaFactura", "ValorFactura", "ValorDepreciado", "UnidadAdministrativaId", "UbicacionId", "EmpleadoId", "ErrorValidacion", "CargaId" };


        //            //    for (int i = 0; i < properties.Length; i++)
        //            //    {
        //            //        if (columnasAIncluir.Contains(properties[i].Name))
        //            //        {
        //            //            headerRow.CreateCell(i).SetCellValue(properties[i].Name);
        //            //        }
        //            //    }

        //            //    // Crear filas de datos
        //            //    for (int i = 0; i < registrosInvalidos.Count; i++)
        //            //    {
        //            //        var row = sheet.CreateRow(i + 1);
        //            //        for (int j = 0; j < properties.Length; j++)
        //            //        {
        //            //            if (columnasAIncluir.Contains(properties[j].Name))
        //            //            {
        //            //                var value = properties[j].GetValue(registrosInvalidos[i]);
        //            //                row.CreateCell(j).SetCellValue(value?.ToString());
        //            //            }
        //            //        }
        //            //    }

        //            //    using (var stream = new MemoryStream())
        //            //    {
        //            //        workbook.Write(stream);
        //            //        excelFile = stream.ToArray();
        //            //    }
        //            //}

        //            using (var workbook = new XSSFWorkbook())
        //            {
        //                var sheet = workbook.CreateSheet("Registros Invalidos");
        //                var headerRow = sheet.CreateRow(0);

        //                // Crear encabezados
        //                var properties = typeof(RegistroBienesTemp).GetProperties();
        //                var columnasAIncluir = new List<string> { "CodigoBien", "NombreBien", "FechaEfectos", "EstatusId", "Descripcion", "Marca", "Modelo", "Serie", "PartidaId", "CambId", "CucopId", "NumeroContrato", "NumeroFactura", "FechaFactura", "ValorFactura", "ValorDepreciado", "UnidadAdministrativaId", "UbicacionId", "EmpleadoId", "ErrorValidacion", "CargaId" };

        //                int columnIndex = 0;
        //                var propertyIndexMap = new Dictionary<int, int>();

        //                for (int i = 0; i < properties.Length; i++)
        //                {
        //                    if (columnasAIncluir.Contains(properties[i].Name))
        //                    {
        //                        headerRow.CreateCell(columnIndex).SetCellValue(properties[i].Name);
        //                        propertyIndexMap[columnIndex] = i;
        //                        columnIndex++;
        //                    }
        //                }

        //                // Crear filas de datos
        //                for (int i = 0; i < registrosInvalidos.Count; i++)
        //                {
        //                    var row = sheet.CreateRow(i + 1);
        //                    foreach (var kvp in propertyIndexMap)
        //                    {
        //                        var value = properties[kvp.Value].GetValue(registrosInvalidos[i]);
        //                        row.CreateCell(kvp.Key).SetCellValue(value?.ToString());
        //                    }
        //                }

        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.Write(stream);
        //                    excelFile = stream.ToArray();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorMessage = $"ErrorNumber: {ex.HResult}, ErrorMessage: {ex.Message}";
        //    }

        //    return (errorMessage, excelFile);
        //}

        //private async Task ProcesarRegistrosBienesAsync(string cargaId, string nombreArchivo)
        //{
        //    try
        //    {
        //        using (var connection = _context.Database.GetDbConnection())
        //        {
        //            if (string.IsNullOrEmpty(connection.ConnectionString))
        //            {
        //                throw new InvalidOperationException("La cadena de conexión está vacía.");
        //            }

        //            if (connection.State != System.Data.ConnectionState.Open)
        //            {
        //                await connection.OpenAsync();
        //            }

        //            using (var command = connection.CreateCommand())
        //            {
        //                command.CommandText = "sp_ProcesarRegistrosBienes";
        //                command.CommandType = System.Data.CommandType.StoredProcedure;

        //                var cargaIdParam = command.CreateParameter();
        //                cargaIdParam.ParameterName = "@CargaId";
        //                cargaIdParam.Value = cargaId;
        //                command.Parameters.Add(cargaIdParam);

        //                var nombreArchivoParam = command.CreateParameter();
        //                nombreArchivoParam.ParameterName = "@NombreArchivo";
        //                nombreArchivoParam.Value = nombreArchivo;
        //                command.Parameters.Add(nombreArchivoParam);

        //                var userNameParam = command.CreateParameter();
        //                userNameParam.ParameterName = "@UserName";
        //                userNameParam.Value = _userContextService.GetUserName();
        //                command.Parameters.Add(userNameParam);

        //                var ipAddressParam = command.CreateParameter();
        //                ipAddressParam.ParameterName = "@IpAddress";
        //                ipAddressParam.Value = _userContextService.GetIpAddress();
        //                command.Parameters.Add(ipAddressParam);

        //                var fechaParam = command.CreateParameter();
        //                fechaParam.ParameterName = "@FECHA";
        //                fechaParam.Value = _userContextService.GetRequestDate() ?? DateTime.Now;
        //                command.Parameters.Add(fechaParam);

        //                // Log para depuración
        //                Console.WriteLine("Ejecutando el procedimiento almacenado con los siguientes parámetros:");
        //                Console.WriteLine($"@CargaId: {cargaId}");
        //                Console.WriteLine($"@NombreArchivo: {nombreArchivo}");
        //                Console.WriteLine($"@UserName: {userNameParam.Value}");
        //                Console.WriteLine($"@IpAddress: {ipAddressParam.Value}");
        //                Console.WriteLine($"@FECHA: {fechaParam.Value}");

        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }
        //    }
        //    catch (DbException dbEx)
        //    {
        //        // Manejo específico para excepciones de base de datos
        //        throw new Exception($"Error de base de datos al procesar los registros: {dbEx.Message}", dbEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo general de excepciones
        //        throw new Exception($"Error al procesar los registros: {ex.Message}", ex);
        //    }
        //}



        public async Task<RegistroBienesDto> CreaRegistroBien(CreaRegistroBienesDto registroBienesDto)
        {
            var registroBienes = _mapper.Map<RegistroBienes>(registroBienesDto);

            registroBienes.UsuarioModifica = _userContextService.GetUserName();
            registroBienes.FechaModificacion = _userContextService.GetRequestDate() ?? DateTime.Now;
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
            registroBienes.FechaModificacion = _userContextService.GetRequestDate() ?? DateTime.Now;
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
            registroBienes.FechaModificacion = _userContextService.GetRequestDate() ?? DateTime.Now;
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

        public async Task<Boolean> FirmaAsignador(byte[] firma, string cadenaOriginal, List<EmpleadoAsigadoDto> empleadosAsigados)
        {
            var usuarioModifica = _userContextService.GetUserName();
            var fechaModificacion = _userContextService.GetRequestDate() ?? DateTime.Now;
            var ipAddress = _userContextService.GetIpAddress();

            // Convertir la lista de empleados asignados a un DataTable para pasarlo al procedimiento almacenado
            var empleadosAsigadosTable = new DataTable();
            empleadosAsigadosTable.Columns.Add("Id", typeof(int));
            empleadosAsigadosTable.Columns.Add("EmpleadoId", typeof(int));
            foreach (var empleado in empleadosAsigados)
            {
                empleadosAsigadosTable.Rows.Add(empleado.Id, empleado.EmpleadoId);
            }

            List<string> correos = new List<string>();

            // Llamar al procedimiento almacenado
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "dbo.sp_ActualizarRegistrosBienes";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Firma", Convert.ToBase64String(firma)));
                command.Parameters.Add(new SqlParameter("@CadenaOriginal", cadenaOriginal));
                command.Parameters.Add(new SqlParameter("@EmpleadosAsignados", empleadosAsigadosTable) { SqlDbType = SqlDbType.Structured, TypeName = "dbo.EmpleadoAsignadoType" });
                command.Parameters.Add(new SqlParameter("@UsuarioModifica", usuarioModifica));
                command.Parameters.Add(new SqlParameter("@FechaModificacion", fechaModificacion));
                command.Parameters.Add(new SqlParameter("@IPAddress", ipAddress));

                await _context.Database.OpenConnectionAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        correos.Add(reader.GetString(0));
                    }
                }
                await _context.Database.CloseConnectionAsync();
            }

            // enviar correos a los empleados
            string subject = "Resguardo de bienes asignados";
            string body = "Se le han asignado bienes para su resguardo, por favor ingresar a su apartado de Resguardo de Bienes para mayor detalle, firmar y aceptar el resguardo, cualquier duda y/o comentario favor de contactar con su Administrador";

            await _correoService.EnviaCorreoOcultoAsync(correos, subject, body);

            return true;
        }


        //public async Task<Boolean> FirmaAsignador(byte[] firma, string cadenaOriginal, List<EmpleadoAsigadoDto> empleadosAsigados)
        //{
        //    // Obtener los IDs de los registros de bienes
        //    var ids = empleadosAsigados.Select(e => e.Id).ToList();

        //    // Obtener los registros de bienes de acuerdo a los IDs
        //    var registrosBienes = await _registroBienesRepository.GetRegistroBienesByIds(ids);

        //    // Crear un diccionario para acceder rápidamente a los EmpleadoAsigadoDto por Id
        //    var empleadosAsigadosDict = empleadosAsigados.ToDictionary(e => e.Id);

        //    // Iniciar una transacción
        //    using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            // Actualizar el campo EmpleadoId de cada registro de bienes
        //            foreach (var registro in registrosBienes)
        //            {
        //                if (empleadosAsigadosDict.TryGetValue(registro.Id, out var empleadoAsigado))
        //                {
        //                    registro.EmpleadoId = empleadoAsigado.EmpleadoId;
        //                    registro.CadenaOriginalEmpleado = cadenaOriginal;
        //                    registro.FirmaEmpleado = Convert.ToBase64String(firma);
        //                    registro.FechaFirmaEmpleado = DateTime.Now;
        //                    registro.UsuarioModifica = _userContextService.GetUserName();
        //                    registro.FechaModificacion = _userContextService.GetRequestDate() ?? DateTime.Now;
        //                    registro.IPAddress = _userContextService.GetIpAddress();
        //                    registro.EstatusId = 1; // se marca como asignado el bien
        //                }
        //            }

        //            // Guardar los cambios en la base de datos
        //            await _context.SaveChangesAsync();

        //            // Confirmar la transacción
        //            await transaction.CommitAsync();

        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            // Revertir la transacción en caso de error
        //            await transaction.RollbackAsync();
        //            throw;
        //        }
        //    }

    //}

        public async Task<IEnumerable<RegistroBienesDto>> ObtieneRegistrosByEmpleadoIdSinFirmar(int empleadoId)
        {
            var registros = await _registroBienesRepository.GetRegistroBienesByEmpleadoIdSinFirmar(empleadoId);

            if (registros == null || !registros.Any())
            {
                throw new ResourceNotFoundException("Registros de Bienes sin firmar");
            }

            return _mapper.Map<IEnumerable<RegistroBienesDto>>(registros);
        }


        public async Task<Boolean> FirmaResguardo(byte[] firma, string cadenaOriginal, List<EmpleadoAsigadoDto> empleadosAsigados)
        {
            // Obtener los IDs de los registros de bienes
            var ids = empleadosAsigados.Select(e => e.Id).ToList();

            // Obtener los registros de bienes de acuerdo a los IDs
            var registrosBienes = await _registroBienesRepository.GetRegistroBienesByIds(ids);

            // Obtener el primer registro de registrosBienes
            var primerRegistro = registrosBienes.FirstOrDefault();

            // Para Obtener el email del empleado
            var emailEmpleado = string.Empty;

            if (primerRegistro != null && primerRegistro.EmpleadoId.HasValue)
            {
                // Obtener el email del empleado usando el servicio de empleado
                var empleado = await _empleadoService.ObtenerPorIdAsync(primerRegistro.EmpleadoId.Value);
                emailEmpleado = empleado?.Email;

                if (!EsCorreoValido(emailEmpleado))
                {
                    throw new ConflictException("La dirección de correo electrónico no es válida. Favor de veriificar tu correo con el Administrador.");
                }
            }

            // Crear un diccionario para acceder rápidamente a los EmpleadoAsigadoDto por Id
            var empleadosAsigadosDict = empleadosAsigados.ToDictionary(e => e.Id);

            // Obtener los IDs de los registros a actualizar
            var idsconcat = string.Join(",", registrosBienes.Select(r => r.Id));

            // Obtener los valores necesarios para la actualización
            var cadena = cadenaOriginal;
            var firmaBase64 = Convert.ToBase64String(firma);
            var fechaFirmaResponsable = DateTime.Now;
            var usuarioModifica = _userContextService.GetUserName();
            var fechaModificacion = _userContextService.GetRequestDate() ?? DateTime.Now;
            var ipAddress = _userContextService.GetIpAddress();
            var estatusId = 1; // se marca como asignado el bien

            // Construir la consulta SQL para la actualización masiva
            var sql = $@"
                UPDATE tbl_registro_bienes
                SET 
                    CadenaOriginalResponsable = @cadenaOriginal,
                    FirmaResponsable = @firmaBase64,
                    FechaFirmaResponsable = @fechaFirmaResponsable,
                    UsuarioModifica = @usuarioModifica,
                    FechaModificacion = @fechaModificacion,
                    IPAddress = @ipAddress,
                    EstatusId = @estatusId
                WHERE Id IN ({idsconcat})";

            // Ejecutar la consulta SQL
            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@cadenaOriginal", cadena),
                new SqlParameter("@firmaBase64", firmaBase64),
                new SqlParameter("@fechaFirmaResponsable", fechaFirmaResponsable),
                new SqlParameter("@usuarioModifica", usuarioModifica),
                new SqlParameter("@fechaModificacion", fechaModificacion),
                new SqlParameter("@ipAddress", ipAddress),
                new SqlParameter("@estatusId", estatusId));


            //// Crear un diccionario para acceder rápidamente a los EmpleadoAsigadoDto por Id
            //var empleadosAsigadosDict = empleadosAsigados.ToDictionary(e => e.Id);

            //// Actualizar el campo EmpleadoId de cada registro de bienes
            //foreach (var registro in registrosBienes)
            //{
            //    if (empleadosAsigadosDict.TryGetValue(registro.Id, out var empleadoAsigado))
            //    {
            //        //registro.EmpleadoId = empleadoAsigado.EmpleadoId;
            //        registro.CadenaOriginalResponsable = cadenaOriginal;
            //        registro.FirmaResponsable = Convert.ToBase64String(firma);
            //        registro.FechaFirmaResponsable = DateTime.Now;
            //        registro.UsuarioModifica = _userContextService.GetUserName();
            //        registro.FechaModificacion = _userContextService.GetRequestDate() ?? DateTime.Now;
            //        registro.IPAddress = _userContextService.GetIpAddress();
            //        registro.EstatusId = 1; /// se marca como asignado el bien
            //    }
            //}

            //// Guardar los cambios en la base de datos
            //await _context.SaveChangesAsync();


            // Agrupar los registros de bienes por CadenaOriginalEmpleado y FirmaEmpleado
            var grupos = registrosBienes
                .GroupBy(r => new { r.CadenaOriginalEmpleado, r.FirmaEmpleado })
                .ToList();


            // Realizar un "join" entre los registros de bienes y los grupos
            var registrosBienesConGrupos = from registro in registrosBienes
                                           join grupo in grupos
                                           on new { registro.CadenaOriginalEmpleado, registro.FirmaEmpleado } equals new { grupo.Key.CadenaOriginalEmpleado, grupo.Key.FirmaEmpleado }
                                           select new
                                           {
                                               Grupo = grupo,
                                               Registro = registro
                                           };


            // Generar un HTML y un PDF para cada grupo
            foreach (var grupo in grupos)
            {

                // Obtener los registros de bienes del grupo actual
                var registrosBienesDelGrupo = registrosBienesConGrupos
                    .Where(r => r.Grupo.Key.CadenaOriginalEmpleado == grupo.Key.CadenaOriginalEmpleado && r.Grupo.Key.FirmaEmpleado == grupo.Key.FirmaEmpleado)
                    .Select(r => r.Registro)
                    .ToList();

                // Generar el contenido HTML para el grupo
                var htmlContent = GenerarHtmlParaGrupo(grupo.Key.CadenaOriginalEmpleado, grupo.Key.FirmaEmpleado,cadenaOriginal,firma, registrosBienesDelGrupo);
                var pdfBytes = _pdfService.GeneratePdf(htmlContent);

                EmailRequestDto emailRequestDto = new EmailRequestDto();
                emailRequestDto.ToEmail = emailEmpleado;
                emailRequestDto.Subject = "Resguardo de bienes asignados";
                emailRequestDto.Body = "Se ha generado un resguardo, se adjunta documento con listado de bienes asignados";

                await _correoService.EnviaCorreoconPdfAsync(emailRequestDto.ToEmail,emailRequestDto.Subject,emailRequestDto.Body, pdfBytes);
            }

            return true;

        }


        private string GenerarHtmlParaGrupo(string cadenaOriginalEmpleado, string firmaEmpleado, string cadenaOriginalResguado, byte[] firmaResguardo, List<RegistroBienes> registrosBienes)
        {
            var columnHeaders = new Dictionary<string, string>
            {
                { "CodigoBien", "Código" },
                { "NombreBien", "Nombre" },
                { "FechaEfectos", "Fecha de Efectos" },
                { "Descripcion", "Descripción" },
                { "EstatusId", "Estatus" },
                { "Serie", "Serie/QR/Cod. de Barras" },
                { "EstadoFisicoId", "Estado Físico" },
                { "Ubicacion", "Ubicación" }
            };

            var html = new StringBuilder();
            html.Append("<html><head><style>");
            html.Append("table { width: 100%; border-collapse: collapse; }");
            html.Append("th, td { border: 1px solid black; padding: 8px; text-align: left; }");
            html.Append(".table-container { height: 70vh; overflow: auto; }");
            html.Append(".signature-container { position: absolute; bottom: 0; width: 100%; height: 30vh; }");
            html.Append("</style></head><body>");
            html.Append("<h1 style='text-align: center;'>Resguardo de bienes asignados</h1>");
            html.Append("<h3>Listado de bienes asignados</h3>");
            html.Append("<div class='table-container'>");
            html.Append("<table>");
            html.Append("<tr>");

            // Agregar encabezados de columnas usando el diccionario
            foreach (var column in columnHeaders.Keys)
            {
                html.Append($"<th>{columnHeaders[column]}</th>");
            }
            html.Append("</tr>");

            // Agregar filas de datos
            foreach (var registro in registrosBienes)
            {
                html.Append("<tr>");
                foreach (var column in columnHeaders.Keys)
                {
                    string value;
                    if (column == "Ubicacion" && registro.Ubicacion != null)
                    {
                        value = registro.Ubicacion.Codigo;
                    }
                    else
                    {
                        value = registro.GetType().GetProperty(column)?.GetValue(registro, null)?.ToString() ?? string.Empty;
                    }

                    html.Append($"<td>{value}</td>");
                }
                html.Append("</tr>");
            }

            html.Append("</table>");
            html.Append("</div>");
            html.Append("<div class='signature-container'>");
            html.Append($"<p>Cadena Original Empleado: {cadenaOriginalEmpleado}</p>");
            html.Append("<div style='word-wrap: break-word; overflow-wrap: break-word; white-space: pre-wrap;'>");
            html.Append($"<p>Firma Empleado: {firmaEmpleado}</p>");
            html.Append("</div>");
            html.Append($"<p>Cadena Original Responsable: {cadenaOriginalResguado}</p>");
            html.Append("<div style='word-wrap: break-word; overflow-wrap: break-word; white-space: pre-wrap;'>");
            html.Append($"<p>Firma Responsable: {Convert.ToBase64String(firmaResguardo)}</p>");
            html.Append("</div>");
            html.Append("</div>");
            html.Append("</body></html>");

            return html.ToString();
        }


        //private string GenerarHtmlParaGrupo(string cadenaOriginalEmpleado, string firmaEmpleado, string cadenaOriginalResguado, byte[] firmaResguardo,  List<RegistroBienes> registrosBienes)
        //{

        //    if (registrosBienes == null || !registrosBienes.Any())
        //    {
        //        throw new Exception("La lista de registros está vacía.");
        //    }

        //    var columnsToInclude = new List<string>
        //        {
        //            "CodigoBien",
        //            "NombreBien",
        //            "FechaEfectos",
        //            "Descripcion",
        //            "EstatusId",
        //            "Serie",
        //            "EstadoFisicoId",
        //            "Ubicacion"
        //        };
        //    var html = new StringBuilder();
        //    html.Append("<h1 style='text-align: center;'>Resguardo de bienes asignados</h1>");

        //    html.Append("<h3>Listado de bienes asignados</h3>");
        //    html.Append("<table border='1'>");
        //    html.Append("<tr>");

        //    // Agregar encabezados de columnas
        //    foreach (var column in columnsToInclude)
        //    {
        //        html.Append($"<th>{column}</th>");
        //    }
        //    html.Append("</tr>");

        //    // Agregar filas de datos
        //    foreach (var registro in registrosBienes)
        //    {
        //        html.Append("<tr>");
        //        foreach (var column in columnsToInclude)
        //        {
        //            string value;
        //            if (column == "Ubicacion" && registro.Ubicacion != null)
        //            {
        //                value = registro.Ubicacion.Codigo; 
        //            }
        //            else {
        //                value = registro.GetType().GetProperty(column)?.GetValue(registro, null)?.ToString() ?? string.Empty;
        //            }

        //            html.Append($"<td>{value}</td>");
        //        }
        //        html.Append("</tr>");
        //    }

        //    html.Append("</table>");
        //    html.Append("<div style='page-break-before: always; position: relative; min-height: 70vh;'>");
        //    html.Append($"<p>Cadena Original Empleado: {cadenaOriginalEmpleado}</p>");
        //    //html.Append($"<p>Firma Empleado: {firmaEmpleado}</p>");
        //    // Agregar la firma del empleado en un contenedor con estilos CSS
        //    html.Append("<div style='word-wrap: break-word; overflow-wrap: break-word; white-space: pre-wrap;'>");
        //    html.Append($"<p>Firma Empleado: {firmaEmpleado}</p>");
        //    html.Append("</div>");

        //    html.Append($"<p>Cadena Original Responsable: {cadenaOriginalResguado}</p>");
        //    //html.Append($"<p>Firma Responsable: {Convert.ToBase64String(firmaResguardo)}</p>");
        //    // Agregar la firma del empleado en un contenedor con estilos CSS
        //    html.Append("<div style='word-wrap: break-word; overflow-wrap: break-word; white-space: pre-wrap;'>");
        //    html.Append($"<p>Firma Empleado: {Convert.ToBase64String(firmaResguardo)}</p>");
        //    html.Append("</div>");
        //    html.Append("</div>");
        //    html.Append("</body></html>");

        //    return html.ToString();
        //}
        private bool EsCorreoValido(string correo)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patron);
        }


    }



}