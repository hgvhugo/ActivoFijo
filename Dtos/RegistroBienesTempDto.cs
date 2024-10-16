namespace ActivoFijo.Dtos
{
    public class RegistroBienesTempDto
    {
        //public int Id { get; set; }
        public string? CodigoBien { get; set; }
        public string? NombreBien { get; set; }
        public DateTime? FechaEfectos { get; set; }
        public int? EstatusId { get; set; }
        //public string? FotoBien { get; set; } 
        public string? Descripcion { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Serie { get; set; }
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
        public bool Procesado { get; set; } = false;// Indica si fue procesado
        //public bool Exito { get; set; } // Indica si fue cargado con éxito
        public string ErrorValidacion { get; set; } // Mensaje de error de validación

        public string NombreArchivo { get; set; } // Nombre del archivo
        public string CargaId { get; set; } // Identificador de la carga
    }
}
