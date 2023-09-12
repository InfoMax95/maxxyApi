namespace maxxyAPI.Entities
{
    public class Code
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
