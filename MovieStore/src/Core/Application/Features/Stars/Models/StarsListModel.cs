using Application.Abstractions.Paging;
using Application.Features.Stars.Dtos;

namespace Application.Features.Stars.Models
{
    public class StarsListModel : BasePageableModel
    {
        public IList<StarsListDto> Items { get; set; } = null!;
    }
}
