namespace Application.Features.Movies.Dtos
{
    public class MovieUpdatedDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IList<MovieStarsListDto> Stars { get; set; } = null!;
        public IList<MovieGenresListDto> Genres { get; set; } = null!;
    }
}
