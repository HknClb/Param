namespace Application.Features.Directors.Dtos
{
    public class CreateDirectorDto
    {
        public CreateDirectorDto() { }

        public CreateDirectorDto(string name, string surname) : this()
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
