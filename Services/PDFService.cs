using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

namespace ActivoFijo.Services
{
    public class PdfService
    {
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdf(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        FooterSettings = { FontSize = 9, Right = "Página [page] de [toPage]", Line = true, Spacing = 2.812 }

                    }
                }
            };

            return _converter.Convert(doc);
        }


        public byte[] GeneratePdfSignature(string htmlContent, List<string> electronicSignatures)
        {
            // Incluir las firmas electrónicas en la parte inferior del contenido HTML
            string signaturesHtml = string.Join("<br/>", electronicSignatures.Select(signature => $"<p>{signature}</p>"));
            string finalHtmlContent = $"{htmlContent}<div style='position: absolute; bottom: 0;'>{signaturesHtml}</div>";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4
        },
                Objects = {
            new ObjectSettings() {
                PagesCount = true,
                HtmlContent = finalHtmlContent,
                WebSettings = { DefaultEncoding = "utf-8" }
            }
        }
            };

            return _converter.Convert(doc);
        }
    }
}
