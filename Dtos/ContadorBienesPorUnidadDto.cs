namespace ActivoFijo.Dtos
{
    namespace ActivoFijo.Dtos
    {
        public class ContadorBienesPorUnidadDto
        {
            public int? UnidadAdministrativaId { get; set; }
            public string NombreUnidadAdministrativa { get; set; } // Nueva propiedad para el nombre
            public int Asignados { get; set; }
            public int NoAsignados { get; set; }
            public int Mantenimiento { get; set; }
            public int Baja { get; set; }
            //public int Total { get; set; }
        }
    }
}
