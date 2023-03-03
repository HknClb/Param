using Domain.Entities.Common;

namespace Domain.Entities
{
    public class MovieStaff : Entity
    {
        public MovieStaff()
        {
        }

        public MovieStaff(string name, string surname) : this()
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;


        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
