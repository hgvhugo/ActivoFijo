using ActivoFijo.Dtos;
using ActivoFijo.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActivoFijo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        // GET: api/Empleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> ObtenerTodos()
        {
            var empleados = await _empleadoService.ObtenerTodosAsync();
            return Ok(empleados);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<EmpleadoDto>> ObtenerPorId(int id)
        {
            var empleado = await _empleadoService.ObtenerPorIdAsync(id);
            return Ok(empleado);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoDto>> Crear([FromBody] CreaEmpleadoDto empleadoDto)
        {
            DatosUsuarioConectadoDto datosUsuario =  new DatosUsuarioConectadoDto();
            datosUsuario.Ip = HttpContext.Items["IpAddress"]?.ToString();
            datosUsuario.Usuario = HttpContext.Items["Usuario"]?.ToString();
            datosUsuario.FechaModificacion = (DateTime?)HttpContext.Items["Fecha"] ?? DateTime.UtcNow;


            var empleado = await _empleadoService.CrearAsync(empleadoDto,datosUsuario);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = empleado.Id }, empleado);

        }


    }
}
