using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivoFijo.Models
{

    [Table("tbl_empleados")]

    public class Empleado
    {
        [Key]
        [Index(nameof(Rfc), IsUnique = true)]
        [Index(nameof(Email), IsUnique = true)]
        public int Id { get; set; }
      
        public string  Rfc { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }


        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioModifica { get; set; }
        public string IPAddress { get; set; }

        public DateTime FechaModificacion { get; set; }

        public ICollection<EmpleadoUnidadAdministrativa> EmpleadoUnidadesAdministrativas { get; set; }

        // Clave foránea para la relación uno a uno
        public int UbicacionId { get; set; }

        // Propiedad de navegación para la relación uno a uno
        public Ubicacion Ubicacion { get; set; }
        public RegistroBienes RegistroBienes { get; set; }


    }
}
