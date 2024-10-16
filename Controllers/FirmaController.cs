using ActivoFijo.Exceptions;
using ActivoFijo.Services;
using ActivoFijo.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivoFijo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirmaController : ControllerBase
    {

        private readonly IFirmaElectronicaService _firmaElectronicaService;

        public FirmaController(IFirmaElectronicaService firmaElectronicaService)
        {
            _firmaElectronicaService = firmaElectronicaService;
        }


        [HttpPost]
        public async Task<IActionResult> FirmarDocumento(IFormFile certificado, IFormFile clavePrivada, string password, IFormFile documento, string cadenaOriginal)
        {
            var firma = await _firmaElectronicaService.FirmarDocumentoAsync(documento, certificado, clavePrivada, password, cadenaOriginal);
            return Ok(Convert.ToBase64String(firma));
        }



        [HttpPost("generar-firma")]
        public async Task<IActionResult> GenerarFirma(IFormFile clavePrivada, string password, string cadenaOriginal)
        {
            var firma = await _firmaElectronicaService.GenerarFirmaAsync(clavePrivada, password, cadenaOriginal);
            return Ok(Convert.ToBase64String(firma));
        }
        
         
    }



}
