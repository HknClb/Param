namespace Application.Features.Stars.Dtos
{
    public class StarsListDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Surname { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<StarringMoviesListDto>? StarringMovies { get; } = new List<StarringMoviesListDto>();
    }
}