namespace Domain.Entities
{
    public class Star : MovieStaff
    {
        public Star() { }

        public Star(string name, string surname) : this()
        {
            Name = name;
            Surname = surname;
        }
    }
}
