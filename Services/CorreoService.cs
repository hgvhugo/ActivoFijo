using ActivoFijo.Services.IServices;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace ActivoFijo.Services
{
    public class CorreoService : ICorreoService
    {

        public async Task EnviaCorreoconPdfAsync(string toEmail, string subject, string body, byte[] pdfBytes)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Activo Fijo Admin", "cicinhocarmona07@gmail.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };

            // Adjuntar el PDF
            builder.Attachments.Add("document.pdf", pdfBytes, new ContentType("application", "pdf"));

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // Ignorar la validación del certificado SSL
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                //await client.ConnectAsync("smtp.example.com", 587, false);
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("cicinhocarmona07@gmail.com", "kndwjnkjxdkkxgue");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }


        public async Task EnviaCorreoOcultoAsync(List<string> toEmails, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Activo Fijo Admin", "cicinhocarmona07@gmail.com"));
            message.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // Ignorar la validación del certificado SSL
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("cicinhocarmona07@gmail.com", "kndwjnkjxdkkxgue");

                // Concatenar todas las direcciones de correo en una sola cadena separada por comas
                var bccAddresses = string.Join(",", toEmails);
                message.Bcc.AddRange(InternetAddressList.Parse(bccAddresses));


                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
