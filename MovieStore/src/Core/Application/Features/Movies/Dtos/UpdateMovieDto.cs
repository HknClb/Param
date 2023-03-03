namespace Application.Features.Movies.Dtos
{
    public class UpdateMovieDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public int? PublishedYear { get; set; }
        public decimal? Price { get; set; }
        public string? DirectorId { get; set; }
        public string[]? StarIds { get; set; }
        public int[]? GenreIds { get; set; }
    }
}
