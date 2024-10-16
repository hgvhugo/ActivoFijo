using ActivoFijo.Services.IServices;
using ActivoFijo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActivoFijo.Dtos;

namespace ActivoFijo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorreoController : ControllerBase
    {
        private readonly ICorreoService _correoService;
        private readonly PdfService _pdfService;

        public CorreoController(ICorreoService correoService, PdfService pdfService)
        {
            _correoService = correoService;
            _pdfService = pdfService;
        }

        [HttpPost("enviar-correo")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto request)
        {
            //string htmlContent = $@"
            //<html>
            //<body>
            //    <h1>{request.Subject}</h1>
            //    <p>{request.Body}</p>
            //    <table border='1'>
            //        <tr>
            //            <th>Column 1</th>
            //            <th>Column 2</th>
            //        </tr>
            //        <tr>
            //            <td>Data 1</td>
            //            <td>Data 2</td>
            //        </tr>
            //    </table>
            //</body>
            //</html>";

            var pdfBytes = _pdfService.GeneratePdf(request.PdfContent);
            await _correoService.EnviaCorreoconPdfAsync(request.ToEmail, request.Subject, request.Body, pdfBytes);
            return Ok("Email sent successfully");
        }

        //public async Task<IActionResult> EnviarCorreo([FromForm] string toEmail, [FromForm] string subject, [FromForm] string body, [FromForm] IFormFile archivo)
        //{
        //    byte[] pdfBytes = null;
        //    if (archivo != null)
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            await archivo.CopyToAsync(ms);
        //            pdfBytes = ms.ToArray();
        //        }
        //    }

        //    await _correoService.EnviaCorreoconPdfAsync(toEmail, subject, body, pdfBytes);
        //    return Ok();
        //}
    }
}
