using System.ComponentModel.DataAnnotations.Schema;

namespace ActivoFijo.Models
{
    [Table("tbr_empleado_unidad_administrativa")]
    public class EmpleadoUnidadAdministrativa
    {
        public int EmpleadoId { get; set; }
         
        public Empleado Empleado { get; set; }

        public int UnidadAdministrativaId { get; set; }

        public UnidadAdministrativa UnidadAdministrativa { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioModifica { get; set; }
        public string IPAddress { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
