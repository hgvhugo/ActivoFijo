namespace ActivoFijo.Dtos
{
    public class FotoBienActivoDto
    {
        public int Id { get; set; }

        public int RegistroBienesId { get; set; }

        public string FotoBien { get; set; }

        public IFormFile FotoBienFile { get; set; }

        public int TipoFotoId { get; set; } // 1 = frente, 2 = trasera, 3 = lateral, 4 = interior , 5 = aerea

    }
}
