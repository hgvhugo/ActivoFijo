namespace ActivoFijo.Services.IServices
{
    public interface IFirmaElectronicaService
    {

        //Task<byte[]> FirmarDocumentoAsync(byte[] documento, byte[] certificado, byte[] clavePrivada, string password, string cadenaOriginal);
        Task<byte[]> FirmarDocumentoAsync(IFormFile documento, IFormFile certificado, IFormFile clavePrivada, string password, string cadenaOriginal);
        Task<byte[]> GenerarFirmaAsync(IFormFile clavePrivada, string password, string cadenaOriginal);

        string GenerarCadenaOriginal(string certificadoFileName); // Nueva firma del método

        //Task<byte[]> GenerarFirmaAsync(Stream clavePrivada, string password, string cadenaOriginal);


    }
}
