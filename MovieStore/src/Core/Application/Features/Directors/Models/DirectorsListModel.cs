using Application.Abstractions.Paging;
using Application.Features.Directors.Dtos;

namespace Application.Features.Directors.Models
{
    public class DirectorsListModel : BasePageableModel
    {
        public IList<DirectorsListDto> Items { get; set; } = null!;
    }
}
