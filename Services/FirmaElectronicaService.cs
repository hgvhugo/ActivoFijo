using ActivoFijo.Exceptions;
using ActivoFijo.Services.IServices;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ActivoFijo.Services
{
    public class FirmaElectronicaService : IFirmaElectronicaService
    {

        public async Task<byte[]> FirmarDocumentoAsync(IFormFile documento, IFormFile certificado, IFormFile clavePrivada, string password, string cadenaOriginal)
        {
            if (certificado == null || clavePrivada == null || documento == null || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(cadenaOriginal))
            {
                throw new BadRequestException("Todos los archivos, la contraseña y la cadena original son requeridos.");
            }

            using (var certificadoStream = certificado.OpenReadStream())
            using (var clavePrivadaStream = clavePrivada.OpenReadStream())
            using (var documentoStream = documento.OpenReadStream())
            {
                var certificadoBytes = new byte[certificado.Length];
                var clavePrivadaBytes = new byte[clavePrivada.Length];
                var documentoBytes = new byte[documento.Length];

                await certificadoStream.ReadAsync(certificadoBytes, 0, (int)certificado.Length);
                await clavePrivadaStream.ReadAsync(clavePrivadaBytes, 0, (int)clavePrivada.Length);
                await documentoStream.ReadAsync(documentoBytes, 0, (int)documento.Length);

                // Cargar la clave privada
                AsymmetricKeyParameter clavePrivadaParam = CargarClavePrivada(new MemoryStream(clavePrivadaBytes), password.ToCharArray());

                if (clavePrivadaParam == null)
                {
                    throw new InvalidOperationException("No se pudo cargar la clave privada.");
                }

                // Crear el firmador
                var firmador = SignerUtilities.GetSigner("SHA256withRSA");
                firmador.Init(true, clavePrivadaParam);

                // Incluir la cadena original en el proceso de firma
                var cadenaOriginalBytes = Encoding.UTF8.GetBytes(cadenaOriginal);
                firmador.BlockUpdate(cadenaOriginalBytes, 0, cadenaOriginalBytes.Length);
                firmador.BlockUpdate(documentoBytes, 0, documentoBytes.Length);

                // Generar la firma
                var firma = firmador.GenerateSignature();

                return firma;
            }
        }



        public async Task<byte[]> GenerarFirmaAsync(IFormFile clavePrivada, string password, string cadenaOriginal)
        {
            if (clavePrivada == null || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(cadenaOriginal))
            {
                throw new BadRequestException("La clave privada, la contraseña y la cadena original son requeridas.");
            }

            using (Stream clavePrivadaStream = clavePrivada.OpenReadStream())
            {

                AsymmetricKeyParameter clavePrivadaParam = CargarClavePrivada(clavePrivadaStream, password.ToCharArray());

                // Cargar la clave privada

                if (clavePrivadaParam == null)
                {
                    throw new InvalidOperationException("No se pudo cargar la clave privada.");
                }

                // Crear el firmador
                var firmador = SignerUtilities.GetSigner("SHA256withRSA");
                firmador.Init(true, clavePrivadaParam);

                // Incluir la cadena original en el proceso de firma
                var cadenaOriginalBytes = Encoding.UTF8.GetBytes(cadenaOriginal);
                firmador.BlockUpdate(cadenaOriginalBytes, 0, cadenaOriginalBytes.Length);

                // Generar la firma
                var firma = firmador.GenerateSignature();

                return firma;

            }
           
        }

        private static AsymmetricKeyParameter CargarClavePrivada(Stream clavePrivadaStream, char[] password)
        {
            try
            {
                // Desencriptar la clave privada
                var clavePrivadaParam = PrivateKeyFactory.DecryptKey(password, clavePrivadaStream);
                return clavePrivadaParam;
            }
            catch (InvalidCipherTextException ex) when (ex.Message.Contains("pad block corrupted"))
            {
                // Capturar la excepción específica y lanzar una excepción personalizada
                throw new BadRequestException("La contraseña es incorrecta.");
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones según sea necesario
                throw new InvalidOperationException("Error al cargar la clave privada: " + ex.Message, ex);
            }
        }
 
 
        private class PasswordFinder : IPasswordFinder
        {
            private readonly char[] password;

            public PasswordFinder(char[] password)
            {
                this.password = password;
            }

            public char[] GetPassword()
            {
                return password;
            }
        }


        public string GenerarCadenaOriginal(string certificadoFileName)
        {
            string rfc = Path.GetFileNameWithoutExtension(certificadoFileName).ToUpper();
            string fechaFirma = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            string idUnico = Guid.NewGuid().ToString("N");

            return $"|3333|{rfc}|{fechaFirma}|{idUnico}|";
        }
    }
}
