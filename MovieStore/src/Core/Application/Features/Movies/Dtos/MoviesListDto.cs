namespace Application.Features.Movies.Dtos
{
    public class MoviesListDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? PublishedYear { get; set; }
        public MovieDirectorDto? Director { get; set; }
        public IList<MovieStarsListDto>? Stars { get; set; }
        public IList<MovieGenresListDto>? Genres { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
