namespace ActivoFijo.Dtos
{
    public class EmailRequestDto
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string PdfContent { get; set; }
    }
}
