using Microsoft.AspNetCore.Identity;

namespace maxxyAPI.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
