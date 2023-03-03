namespace Application.Features.Directors.Dtos
{
    public class DirectorCreatedDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
