using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Movie : Entity
    {
        public Movie() { }

        public Movie(Guid directorId, string name, int publishedYear, decimal price) : this()
        {
            DirectorId = directorId;
            Name = name;
            PublishedYear = publishedYear;
            Price = price;
        }

        public Guid DirectorId { get; set; }
        public string Name { get; set; } = null!;
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }

        public virtual Director Director { get; set; } = null!;
        public virtual ICollection<Star> Stars { get; set; } = new List<Star>();
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
