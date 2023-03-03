namespace Domain.Entities
{
    public class Director : MovieStaff
    {
        public Director() { }

        public Director(string name, string surname) : this()
        {
            Name = name;
            Surname = surname;
        }
    }
}