using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivoFijo.Models
{
    [Table("tbl_registro_bienes_log")]

    public class RegistroBienesLog
    {
        [Key]
        public int LogId { get; set; }

        public int Id { get; set; }

        public string CodigoBien { get; set; }

        public string NombreBien { get; set; }

        public DateTime FechaEfectos { get; set; }

        public int EstatusId { get; set; } = 0;  //FK a 0.- No Asignado, 1.- Asignado, 2.- Mantenimiento 

        public string? FotoBien { get; set; }

        public string? Descripcion { get; set; }

        public string? Marca { get; set; }

        public string? Modelo { get; set; }
        public string? Serie { get; set; }

        public int EstadoFisicoId { get; set; } //FK a  0.Malo, 1.Regular, 2.Bueno
        public int? PartidaId { get; set; } //FK a cat_partidas

        public int? CambId { get; set; } //FK a cat_cambs


        public int? CucopId { get; set; } //FK a cat_cucop

        public string? NumeroContrato { get; set; }

        public string? NumeroFactura { get; set; }

        public DateTime? FechaFactura { get; set; }

        public double? ValorFactura { get; set; }

        public double? ValorDepreciado { get; set; }


        public int? UnidadAdministrativaId { get; set; } //FK a cat_unidades_administrativas

        public int? UbicacionId { get; set; } //FK a cat_ubicaciones 

        public int? EmpleadoId { get; set; } //FK a cat_empleados 

        public int? FotosId { get; set; } //FK a tbl_fotos_bien_activo


        public string? FirmaEmpleado { get; set; }

        public string? CadenaOriginalEmpleado { get; set; }

        public DateTime? FechaFirmaEmpleado { get; set; }

        public string? FirmaResponsable { get; set; }

        public string? CadenaOriginalResponsable { get; set; }

        public DateTime? FechaFirmaResponsable { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public string? UsuarioModifica { get; set; }
        public string? IPAddress { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? NombreArchivo { get; set; }
        public string? cargaId { get; set; }

        public string? motivo { get; set; }
        public string? UsuarioModificaLog { get; set; }
        public string? IPAddressLog { get; set; }

        public DateTime? FechaModificacionLog { get; set; }

    }
}
