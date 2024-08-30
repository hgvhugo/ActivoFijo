namespace ActivoFijo.Dtos
{
    public class EmpleadoUnidadAdministrativaDto
    {
         public int EmpleadoId { get; set; }

        public EmpleadoDto Empleado { get; set; }

        public int UnidadAdministrativaId { get; set; }

        public UnidadAdministrativaDto UnidadAdministrativa { get; set; }
    }
}
