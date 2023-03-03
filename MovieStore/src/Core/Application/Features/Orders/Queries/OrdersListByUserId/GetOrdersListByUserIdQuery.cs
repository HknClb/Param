using Application.Abstractions.Services;
using Application.DynamicQuery;
using Application.Features.Orders.Models;
using Application.Requests;
using MediatR;

namespace Application.Features.Orders.Queries.OrdersListByUserId
{
    public class GetOrdersListByUserIdQuery : IRequest<OrdersListModel>
    {
        public string UserId { get; set; } = null!;
        public Dynamic? Dynamic { get; set; }
        public PageRequest? PageRequest { get; set; }

        public class GetOrdersListByUserIdQueryHandler : IRequestHandler<GetOrdersListByUserIdQuery, OrdersListModel>
        {
            private readonly IOrderService _orderService;

            public GetOrdersListByUserIdQueryHandler(IOrderService orderService)
            {
                _orderService = orderService;
            }

            public async Task<OrdersListModel> Handle(GetOrdersListByUserIdQuery request, CancellationToken cancellationToken)
                => await _orderService.GetOrdersByUserId(request.UserId, request.Dynamic ?? new(), request.PageRequest ?? new(), cancellationToken);
        }
    }
}
