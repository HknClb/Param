namespace Application.Features.Directors.Dtos
{
    public class UpdateDirectorDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public bool IsActive { get; set; }
    }
}
