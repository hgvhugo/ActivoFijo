using System.ComponentModel.DataAnnotations;

namespace ActivoFijo.Dtos
{
    public class CreaRegistroBienesDto
    {
        
        [MaxLength(15, ErrorMessage = "El código no puede tener más de 15 caracteres.")]
        public string CodigoBien { get; set; }

        [Required(ErrorMessage = "El nombre del bien es requerido.")]
        [MaxLength(100, ErrorMessage = "El nombre del bien no puede tener más de 100 caracteres.")]
        public string NombreBien { get; set; }

        public DateTime FechaEfectos { get; set; }

        public int EstatusId { get; set; }

        public string? FotoBien { get; set; }

        public IFormFile? FotoBienFile { get; set; }


        [MaxLength(150, ErrorMessage = "La descripción no puede tener más de 150 caracteres.")]
        public string? Descripcion { get; set; }

        [MaxLength(50, ErrorMessage = "La marca no puede tener más de 50 caracteres.")]
        public string? Marca { get; set; }

        [MaxLength(50, ErrorMessage = "El modelo no puede tener más de 50 caracteres.")]
        public string? Modelo { get; set; }

        [MaxLength(50, ErrorMessage = "El número de serie no puede tener más de 50 caracteres.")]
        public string?   Serie { get; set; }

        public int? PartidaId { get; set; }

        public int? CambId { get; set; }

        public int? CucopId { get; set; }

        public string? NumeroContrato { get; set; }

        public string? NumeroFactura { get; set; }

        public DateTime? FechaFactura { get; set; }

        public double? ValorFactura { get; set; }

        public double? ValorDepreciado { get; set; }

        public int? UnidadAdministrativaId { get; set; }

        public int? UbicacionId { get; set; }

        public int? EmpleadoId { get; set; }

        public int? FotosId { get; set; }

        
 
    }
}
