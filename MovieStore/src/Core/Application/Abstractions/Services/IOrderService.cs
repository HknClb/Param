using Application.DynamicQuery;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Requests;

namespace Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<OrderPlacedDto> PlaceOrderAsync(PlaceOrderDto placeOrder);
        Task<OrdersListModel> GetOrdersByUserId(string userId, Dynamic dynamic, PageRequest pageRequest, CancellationToken cancellationToken = default);
    }
}
