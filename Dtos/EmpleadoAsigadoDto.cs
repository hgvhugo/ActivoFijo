using System.Text.Json.Serialization;

namespace ActivoFijo.Dtos
{
    public class EmpleadoAsigadoDto
    {
        [JsonPropertyName("id")]

        public int Id { get; set; }

        [JsonPropertyName("empleadoId")]

        public int EmpleadoId { get; set; } = 0;
    }
}
