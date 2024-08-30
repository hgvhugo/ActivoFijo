using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivoFijo.Models
{
    [Table("tbl_fotos_bien_activo")]
    public class FotosBienActivo
    {
        [Key]
        public int Id { get; set; }

        public int  RegistroBienesId { get; set; }

        public string FotoBien { get; set; }

        public int TipoFotoId { get; set; } // 1 = frente, 2 = trasera, 3 = lateral, 4 = interior , 5 = aerea

        public RegistroBienes RegistroBienes { get; set; }

    }
}
