namespace ActivoFijo.Dtos
{
    public class CreaEmpleadoDto
    {
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int UbicacionId { get; set; }
        public ICollection<int> EmpleadoUnidadesAdministrativasIds { get; set; }

    }
}
