using maxxyAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace maxxyAPI.DTOs
{
    public class PostDto
    {
        public string Title { get; set; } = string.Empty;
        public string FirstContent { get; set; } = string.Empty;
        public string SecondContent { get; set; } = string.Empty;
        public string ThirdContent { get; set; } = string.Empty;
        public List<Code> Codes { get; set; } = new List<Code>();
        public Image Image { get; set; } = new Image();
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Created { get; set; } = DateTime.UtcNow.ToLongDateString();
        public string Updated { get; set; } = DateTime.UtcNow.ToLongDateString();
    }
}
