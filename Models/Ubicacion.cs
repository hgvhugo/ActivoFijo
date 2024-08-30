using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivoFijo.Models
{
    [Table("cat_ubicaciones")]
    public class Ubicacion
    {
        [Key]
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioModifica { get; set; }
        public string IPAddress { get; set; }

        public DateTime FechaModificacion { get; set; }

        public ICollection<Empleado> Empleados { get; set; }
        public ICollection<RegistroBienes> RegistroBienes { get; set; }

    }
}
