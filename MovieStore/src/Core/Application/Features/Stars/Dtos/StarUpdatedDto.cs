namespace Application.Features.Stars.Dtos
{
    public class StarUpdatedDto
    {
        public virtual Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<StarringMoviesListDto> StarringMovies { get; } = new List<StarringMoviesListDto>();
    }
}
