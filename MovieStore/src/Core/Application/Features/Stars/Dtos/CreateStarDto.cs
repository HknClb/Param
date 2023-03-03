namespace Application.Features.Stars.Dtos
{
    public class CreateStarDto
    {
        public CreateStarDto() { }

        public CreateStarDto(string name, string surname) : this()
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
