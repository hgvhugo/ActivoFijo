using ActivoFijo.Dtos;

namespace ActivoFijo.Services.IServices
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoDto>> ObtenerTodosAsync();
        Task<EmpleadoDto> ObtenerPorIdAsync(int id);

        Task<EmpleadoDto> CrearAsync(CreaEmpleadoDto empleadoDto,DatosUsuarioConectadoDto datosUsuarioConectado);

        Task<IEnumerable<EmpleadoDto>> ObtenerTodosporUnidad(int unidadAdministrativaId);

    }
}
