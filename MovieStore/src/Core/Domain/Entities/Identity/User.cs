using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public User()
        {
        }

        public User(string? userName = null) : base(userName)
        {
        }

        public User(string userName, string email, string name, string surname) : this(userName)
        {
            Email = email;
            Name = name;
            Surname = surname;
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Order> Baskets { get; set; } = new List<Order>();
        public virtual ICollection<Genre> FavoriteGenres { get; set; } = new List<Genre>();
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
