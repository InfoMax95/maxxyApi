namespace maxxyAPI.DTOs
{
    public class ImageDto
    {
        public string Url { get; set; }
        public bool IsMain { get; set; } = false;
        public string? PublicId { get; set; }
    }
}
