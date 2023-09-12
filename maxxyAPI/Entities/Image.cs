namespace maxxyAPI.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; } = false;
        public string? PublicId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
