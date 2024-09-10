using ActivoFijo.Dtos;
using ActivoFijo.Dtos.ActivoFijo.Dtos;
using ActivoFijo.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivoFijo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroBienesController : ControllerBase
    {
        private readonly IRegistroBienesService _registroBienesService;

        public RegistroBienesController(IRegistroBienesService registroBienesService)
        {
            _registroBienesService = registroBienesService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistroBienes(int id)
        {
            RegistroBienesDto registroBienes = await _registroBienesService.GetRegistroBienes(id);
            return Ok(registroBienes);
        }


        [HttpPost]
        public async Task<IActionResult> CreaRegistroBien([FromBody] CreaRegistroBienesDto registroBienesDto)
        {
            RegistroBienesDto registroBienes = await _registroBienesService.CreaRegistroBien(registroBienesDto);
            return Ok(registroBienes);
        }

        [HttpPut]

        public async Task<IActionResult> ActualizaRegistroBien([FromBody] ActualizaRegistroBienesDto registroBienesDto)
        {
            RegistroBienesDto registroBienes = await _registroBienesService.ActualizaRegistroBien(registroBienesDto);
            return Ok(registroBienes);
        }

        [HttpDelete("{registroBienesId}")]
        public async Task<IActionResult> EliminaRegistroBien(int registroBienesId)
        {
            RegistroBienesDto registroBienes = await _registroBienesService.EliminaRegistroBien(registroBienesId);
            return Ok(registroBienes);
        }


        [HttpPost("cargar-excel")]
        public async Task<IActionResult> CargarExcelAsync([FromForm] IFormFile archivo)
        {
           

            List<RegistroBienesTempDto> registrosconerror = await _registroBienesService.CargarExcelAsync(archivo);
            return Ok(registrosconerror);
        }


        [HttpGet("unidad/{unidadAdministrativaId}")]
        public async Task<IActionResult> GetRegistroBienesByUnidadAdministrativaId(int unidadAdministrativaId)
        {
            List<RegistroBienesDto> registros = await _registroBienesService.GetRegistroBienesByUnidadAdministrativaId(unidadAdministrativaId);
            return Ok(registros);
        }

        [HttpGet("buscar")]
        public
            async Task<IActionResult> GetRegistroBienesByCriteria([FromQuery] int unidadAdministrativaId, [FromQuery] int? estadoId = null, [FromQuery] int? partidaId = null, [FromQuery] int? estatusId = null, [FromQuery] int? cambId = null, [FromQuery] string? contrato = null, [FromQuery] int? cucopId = null)
        {
            IEnumerable<RegistroBienesDto> registros = await _registroBienesService.GetRegistroBienesByCriteria(unidadAdministrativaId, estadoId, partidaId, estatusId, cambId, contrato, cucopId);
            return Ok(registros);
        }

        [HttpGet("conteos")]
        public async Task<IActionResult> ObtenerConteosPorUnidadAsync()
        {
            IEnumerable<ContadorBienesPorUnidadDto> conteos = await _registroBienesService.ObtenerConteosPorUnidadAsync();
            return Ok(conteos);
        }
    }
}
