namespace ActivoFijo.Services.IServices
{
    public interface ICorreoService

    {
        Task EnviaCorreoconPdfAsync(string toEmail, string subject, string body, byte[] pdfBytes);
        Task EnviaCorreoOcultoAsync(List<string> toEmails, string subject, string body);

    }
}
