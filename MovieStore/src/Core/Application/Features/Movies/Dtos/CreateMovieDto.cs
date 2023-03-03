namespace Application.Features.Movies.Dtos
{
    public class CreateMovieDto
    {
        public string Name { get; set; } = null!;
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public string DirectorId { get; set; } = null!;
        public string[] StarIds { get; set; } = null!;
        public int[] GenreIds { get; set; } = null!;
    }
}
