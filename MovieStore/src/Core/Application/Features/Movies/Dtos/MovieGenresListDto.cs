namespace Application.Features.Movies.Dtos
{
    public class MovieGenresListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
