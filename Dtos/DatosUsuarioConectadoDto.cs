namespace ActivoFijo.Dtos
{
    public class DatosUsuarioConectadoDto
    {
        public string Ip { get; set; }

        public string Usuario { get; set; }

        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
