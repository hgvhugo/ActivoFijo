using ActivoFijo.Data;
using ActivoFijo.Dtos;
using ActivoFijo.Exceptions;
using ActivoFijo.Models;
using ActivoFijo.Repositories.IRepository;
using ActivoFijo.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ActivoFijo.Services
{
    public class EmpleadoService : IEmpleadoService


    {
        private readonly IRepositorioGenerico<Empleado> _repositorioEmpleado;
        private readonly IRepositorioGenerico<EmpleadoUnidadAdministrativa> _repositorioRelacion;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ILogger<EmpleadoService> _logger;

        public EmpleadoService(
            IRepositorioGenerico<Empleado> repositorioEmpleado,
            IRepositorioGenerico<EmpleadoUnidadAdministrativa> repositorioRelacion,
            IMapper mapper,
            AppDbContext context,
            ILogger<EmpleadoService> logger)
        {
            _repositorioEmpleado = repositorioEmpleado;
            _repositorioRelacion = repositorioRelacion;
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<EmpleadoDto>> ObtenerTodosAsync()
        {

            IEnumerable<Empleado> listaEmpleados = await _repositorioEmpleado.GetAllAsync();

            if (listaEmpleados == null || !listaEmpleados.Any())
            {
                throw new ResourceNotFoundException("Empleados");
            }

            IEnumerable<EmpleadoDto> listaEmpleadosDto = _mapper.Map<IEnumerable<EmpleadoDto>>(listaEmpleados);

            return listaEmpleadosDto;
        }

        public async Task<EmpleadoDto> ObtenerPorIdAsync(int id)
        {
            Empleado empleado = await _repositorioEmpleado.GetByIdAsync(id);

            if (empleado == null)
            {
                throw new ResourceNotFoundException("Empleado", "id",id);
            }

            EmpleadoDto empleadoDto = _mapper.Map<EmpleadoDto>(empleado);
            return empleadoDto;

        }

        public async Task<EmpleadoDto> CrearAsync(CreaEmpleadoDto empleadoDto,DatosUsuarioConectadoDto datosUsuarioConectado)
        {

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _logger.LogInformation("Iniciando transacción para crear empleado.");

                    Empleado empleado = _mapper.Map<Empleado>(empleadoDto);
                    empleado.IPAddress = datosUsuarioConectado.Ip;
                    empleado.UsuarioModifica = datosUsuarioConectado.Usuario;
                    empleado.FechaModificacion = datosUsuarioConectado.FechaModificacion;
                    empleado.FechaCreacion = DateTime.UtcNow;

                    await _repositorioEmpleado.AddAsync(empleado);
                    await _context.SaveChangesAsync(); // Asegúrate de guardar los cambios para obtener el ID del empleado

                    // Verificar que el ID del empleado se ha generado
                    if (empleado.Id == 0)
                    {
                        throw new Exception("El ID del empleado no se ha generado correctamente.");
                    }

                    // Insertar en la tabla intermedia
                    foreach (var unidadId in empleadoDto.EmpleadoUnidadesAdministrativasIds)
                    {
                        var empleadoUnidad = new EmpleadoUnidadAdministrativa
                        {
                            EmpleadoId = empleado.Id,
                            UnidadAdministrativaId = unidadId,
                            Activo = true,
                            FechaCreacion = DateTime.UtcNow,
                            UsuarioModifica = datosUsuarioConectado.Usuario,
                            IPAddress = datosUsuarioConectado.Ip,
                            FechaModificacion = datosUsuarioConectado.FechaModificacion
                        };
                        await _repositorioRelacion.AddAsync(empleadoUnidad);
                    }

                    await _context.SaveChangesAsync(); // Guardar los cambios para las relaciones
                    await transaction.CommitAsync(); // Confirmar la transacción
                    _logger.LogInformation("Transacción confirmada para el empleado con ID: {EmpleadoId}", empleado.Id);

                    
                    EmpleadoDto empleadoDtoResult = _mapper.Map<EmpleadoDto>(empleado);
                    Ubicacion ubicacion = await _context.Ubicaciones.FindAsync(empleado.UbicacionId);
                    empleadoDtoResult.Ubicacion = _mapper.Map<UbicacionDto>(ubicacion);
                    ICollection<EmpleadoUnidadAdministrativa> empleadoUnidades = await _context.EmpleadoUnidadesAdministrativas
                        .Where(eu => eu.EmpleadoId == empleado.Id)
                        .ToListAsync();
                    empleadoDtoResult.EmpleadoUnidadesAdministrativas = _mapper.Map<List<EmpleadoUnidadAdministrativaDto>>(empleadoUnidades);

                    return empleadoDtoResult;
                    //return _mapper.Map<EmpleadoDto>(empleado);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear el empleado y sus relaciones. Revirtiendo transacción.");

                    await transaction.RollbackAsync(); // Revertir la transacción en caso de error
                    throw new Exception("Error al crear el empleado y sus relaciones: " + ex.Message, ex);
                }
            }

        }


        public async Task<IEnumerable<EmpleadoDto>> ObtenerTodosporUnidad(int unidadAdministrativaId)
        {
            // Paso 1: Obtener las relaciones que coincidan con el unidadAdministrativaId
            var relaciones = await _repositorioRelacion.GetAllByCriteriaAsync(eu => eu.UnidadAdministrativaId == unidadAdministrativaId && eu.Activo);

            if (relaciones == null || !relaciones.Any())
            {
                throw new ResourceNotFoundException("Relaciones de EmpleadoUnidadAdministrativa", "unidadAdministrativaId", unidadAdministrativaId);
            }

            // Paso 2: Obtener los empleados correspondientes a los EmpleadoId obtenidos en el paso anterior
            var empleadoIds = relaciones.Select(r => r.EmpleadoId).Distinct();
            var empleados = await _repositorioEmpleado.GetAllByCriteriaAsync(e => empleadoIds.Contains(e.Id));

            if (empleados == null || !empleados.Any())
            {
                throw new ResourceNotFoundException("Empleados", "unidadAdministrativaId", unidadAdministrativaId);
            }

            // Paso 3: Mapear a DTOs
            var empleadosDto = _mapper.Map<IEnumerable<EmpleadoDto>>(empleados);

            return empleadosDto;


        }
    }

}

