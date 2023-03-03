namespace Application.Features.Stars.Dtos
{
    public class UpdateStarDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public bool IsActive { get; set; }
    }
}
