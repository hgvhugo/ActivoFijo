using ActivoFijo.Dtos;
using ActivoFijo.Exceptions;
using ActivoFijo.Models;
using ActivoFijo.Repositories.IRepository;
using ActivoFijo.Services.IServices;
using AutoMapper;

namespace ActivoFijo.Services
{
    public class EmpleadoService : IEmpleadoService

    {
        private readonly IRepositorioGenerico<Empleado> _repositorioEmpleado;
        private readonly IMapper _mapper;

        public EmpleadoService(IRepositorioGenerico<Empleado> repositorioEmpleado, IMapper mapper)
        {
            _repositorioEmpleado = repositorioEmpleado;
            _mapper = mapper;
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
            Empleado empleado = _mapper.Map<Empleado>(empleadoDto);
            empleado.IPAddress = datosUsuarioConectado.Ip;
            empleado.UsuarioModifica = datosUsuarioConectado.Usuario;
            empleado.FechaModificacion = datosUsuarioConectado.FechaModificacion;
            empleado.FechaCreacion = DateTime.UtcNow;

            await _repositorioEmpleado.AddAsync(empleado);
            return _mapper.Map<EmpleadoDto>(empleado);
            

        }
    }
}
