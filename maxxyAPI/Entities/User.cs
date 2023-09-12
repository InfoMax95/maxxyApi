using Microsoft.AspNetCore.Identity;

namespace maxxyAPI.Entities
{
    public class User : IdentityUser<int>
    {
        public List<Post> Posts { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
