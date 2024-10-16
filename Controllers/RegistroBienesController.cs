using ActivoFijo.Dtos;
using ActivoFijo.Dtos.ActivoFijo.Dtos;
using ActivoFijo.Services;
using ActivoFijo.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;


namespace ActivoFijo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroBienesController : ControllerBase
    {
        private readonly IRegistroBienesService _registroBienesService;
        private readonly IFirmaElectronicaService _firmaElectronicaService;
        private readonly ICorreoService _correoService;
        private readonly PdfService _pdfService;

        public RegistroBienesController(IRegistroBienesService registroBienesService
                , IFirmaElectronicaService firmaElectronicaService, ICorreoService correoService
                , PdfService pdfService
            )
        {
            _registroBienesService = registroBienesService;
            _firmaElectronicaService = firmaElectronicaService;
            _correoService = correoService;
            _pdfService = pdfService;
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
        public async Task<IActionResult> CargarExcelAsync(IFormFile archivo)
        {


            var resultado = await _registroBienesService.CargarExcelResult(archivo);
            return Ok(resultado);
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


        [HttpPut("firmaAsignador")]
        public async Task<IActionResult> FirmaAsignador(IFormFile certificado, IFormFile clavePrivada, [FromForm] string password, [FromForm] string datosFirmar)
        {


            List<EmpleadoAsigadoDto> empleadosAsignados;
            try
            {
                empleadosAsignados = JsonSerializer.Deserialize<List<EmpleadoAsigadoDto>>(datosFirmar);
            }
            catch (JsonException ex)
            {
                return BadRequest($"Error al deserializar datosFirmar: {ex.Message}");
            }

            if (empleadosAsignados == null)
            {
                return BadRequest("La lista de empleados asignados no puede ser nula.");
            }

            string cadenaOriginal = _firmaElectronicaService.GenerarCadenaOriginal(certificado.FileName);

            byte[] firma;
  
            firma = await _firmaElectronicaService.GenerarFirmaAsync(clavePrivada, password, cadenaOriginal);
 

            bool resultado;
            
            resultado = await _registroBienesService.FirmaAsignador(firma, cadenaOriginal, empleadosAsignados);
             

            if (resultado)
            {
                return Ok(new { estatusCode = 200, Message = "Firma Exitosa" });
            }
            else
            {
                return StatusCode(500, new { estatusCode = 500, Message = "Error al firmar los registros de bienes" });
            }


        }

        [HttpGet("registrosSinFirmar/{empleadoId}")]
        public async Task<IActionResult> GetRegistroBienesByEmpleadoIdSinFirmar(int empleadoId)
        {
            IEnumerable<RegistroBienesDto> registros = await _registroBienesService.ObtieneRegistrosByEmpleadoIdSinFirmar(empleadoId);
            return Ok(registros);

        }



        [HttpPut("firmaResguardo")]
        public async Task<IActionResult> FirmaResguardo(IFormFile certificado, IFormFile clavePrivada, [FromForm] string password, [FromForm] string datosFirmar)
        {


            List<EmpleadoAsigadoDto> empleadosAsignados;
            try
            {
                empleadosAsignados = JsonSerializer.Deserialize<List<EmpleadoAsigadoDto>>(datosFirmar);
            }
            catch (JsonException ex)
            {
                return BadRequest($"Error al deserializar datosFirmar: {ex.Message}");
            }

            if (empleadosAsignados == null)
            {
                return BadRequest("La lista de empleados asignados no puede ser nula.");
            }

            string cadenaOriginal = _firmaElectronicaService.GenerarCadenaOriginal(certificado.FileName);

            byte[] firma;
    
            firma = await _firmaElectronicaService.GenerarFirmaAsync(clavePrivada, password, cadenaOriginal);
         

            bool resultado;
           
                resultado = await _registroBienesService.FirmaResguardo(firma, cadenaOriginal, empleadosAsignados);
         

            if (resultado)
            {
                return Ok(new { estatusCode = 200, Message = "Firma Exitosa" });
            }
            else
            {
                return StatusCode(500, new { estatusCode = 500, Message = "Error al firmar los registros de bienes" });
            }


        }

        [HttpPost("correoResguardo")]
        public async Task<IActionResult> CorreoResguardo([FromBody] EmailRequestDto request)
        {
            var pdfBytes = _pdfService.GeneratePdf(request.PdfContent);
            await _correoService.EnviaCorreoconPdfAsync(request.ToEmail, request.Subject, request.Body, pdfBytes);
            return Ok("Email sent successfully");
        }

    }
}
