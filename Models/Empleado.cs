using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivoFijo.Models
{

    [Table("tbl_empleados")]

    [Index(nameof(Rfc), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string  Rfc { get; set; }

        public string? Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }


        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        [Required]
        public bool Activo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public string UsuarioModifica { get; set; }
        [Required]
        public string IPAddress { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }

        public ICollection<EmpleadoUnidadAdministrativa>? EmpleadoUnidadesAdministrativas { get; set; }

        // Clave foránea para la relación uno a uno
        public int? UbicacionId { get; set; }

        // Propiedad de navegación para la relación uno a uno
        public Ubicacion? Ubicacion { get; set; }
        //public RegistroBienes? RegistroBienes { get; set; }


    }
}
