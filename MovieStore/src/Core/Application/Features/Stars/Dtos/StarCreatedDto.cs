namespace Application.Features.Stars.Dtos
{
    public class StarCreatedDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}