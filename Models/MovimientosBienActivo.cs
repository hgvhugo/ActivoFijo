using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivoFijo.Models
{
    [Table("tbl_movimientos_bien_activo")]
    public class MovimientosBienActivo
    {


        [Key]
        public int Id { get; set; }

        [MaxLength(15, ErrorMessage = "El código no puede tener más de 15 caracteres.")]
        public string CodigoBien { get; set; }

        [Required(ErrorMessage = "El nombre del bien es requerido.")]
        [MaxLength(100, ErrorMessage = "El nombre del bien no puede tener más de 100 caracteres.")]
        public string NombreBien { get; set; }

        public DateTime FechaEfectos { get; set; }

        public int EstatusId { get; set; }  //FK a 0.- No Asignado, 1.- Asignado, 2.- Mantenimiento 

        public string FotoBien { get; set; }

        [MaxLength(150, ErrorMessage = "La descripción no puede tener más de 150 caracteres.")]
        public string Descripcion { get; set; }

        [MaxLength(50, ErrorMessage = "La marca no puede tener más de 50 caracteres.")]
        public string Marca { get; set; }

        [MaxLength(50, ErrorMessage = "El modelo no puede tener más de 50 caracteres.")]
        public string Modelo { get; set; }
        [MaxLength(50, ErrorMessage = "El número de serie no puede tener más de 50 caracteres.")]
        public string Serie { get; set; }


        public int PartidaId { get; set; } //FK a cat_partidas

        public int CambId { get; set; } //FK a cat_cambs


        public int CucopId { get; set; } //FK a cat_cucop

        public string NumeroContrato { get; set; }

        public string NumeroFactura { get; set; }

        public DateTime FechaFactura { get; set; }

        public double ValorFactura { get; set; }

        public double ValorDepreciado { get; set; }


        public int UnidadAdministrativaId { get; set; } //FK a cat_unidades_administrativas

        public int UbicacionId { get; set; } //FK a cat_ubicaciones 

        public int EmpleadoId { get; set; } //FK a cat_empleados 

        public int FotosId { get; set; } //FK a tbl_fotos_bien_activo


        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public string UsuarioModifica { get; set; }
        public string IPAddress { get; set; }

        public DateTime FechaModificacion { get; set; }

        

    }
}
