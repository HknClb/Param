using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public virtual ICollection<User> Users { get; } = new List<User>();
    }
}
