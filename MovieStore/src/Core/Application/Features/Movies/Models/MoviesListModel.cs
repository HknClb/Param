using Application.Abstractions.Paging;
using Application.Features.Movies.Dtos;

namespace Application.Features.Movies.Models
{
    public class MoviesListModel : BasePageableModel
    {
        public IList<MoviesListDto> Items { get; set; } = null!;
    }
}
