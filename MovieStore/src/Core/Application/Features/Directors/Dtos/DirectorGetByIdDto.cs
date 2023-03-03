namespace Application.Features.Directors.Dtos
{
    public class DirectorGetByIdDto
    {
        public virtual Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<MoviesDirectedListDto> MoviesDirected { get; } = new List<MoviesDirectedListDto>();
    }
}
