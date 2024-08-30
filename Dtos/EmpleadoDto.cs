namespace ActivoFijo.Dtos
{
    public class EmpleadoDto
    {

        public int Id { get; set; }

        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
             
        public UbicacionDto Ubicacion { get; set; }

        public ICollection<EmpleadoUnidadAdministrativaDto> EmpleadoUnidadesAdministrativas { get; set; }

    }
}
