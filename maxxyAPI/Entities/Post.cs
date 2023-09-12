namespace maxxyAPI.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FirstContent { get; set; } = string.Empty;
        public string SecondContent { get; set; } = string.Empty;
        public string ThirdContent { get; set; } = string.Empty;
        public List<Code> Codes { get; set; } = new List<Code>();
        public Image Image { get; set; } = new Image();
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Created { get; set; } = DateTime.UtcNow.ToLongDateString();
        public string Updated { get; set; } = DateTime.UtcNow.ToLongDateString();
    }
}
