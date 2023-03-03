using Domain.Entities.Common;
using Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Genre : Entity
    {
        public Genre() { }

        public Genre(string title, string? description = null) : this()
        {
            Title = title;
            Description = description;
        }

        public new int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public virtual ICollection<User> FavoriteCustomers { get; set; } = new List<User>();

        [NotMapped] public override DateTime CreatedDate { get => base.CreatedDate; set => base.CreatedDate = value; }
        [NotMapped] public override DateTime? UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
        [NotMapped] public override bool IsActive { get => base.IsActive; set => base.IsActive = value; }
    }
}
