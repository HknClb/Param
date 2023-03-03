using Application.Abstractions.Paging;
using Application.Features.Orders.Dtos;

namespace Application.Features.Orders.Models
{
    public class OrdersListModel : BasePageableModel
    {
        public IList<OrdersListDto> Items { get; set; } = null!;
    }
}
