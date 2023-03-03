using Application.Abstractions.Services;
using Application.Features.Orders.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Orders.Commands.PlaceOrder
{
    public class PlaceOrderCommand : IRequest<OrderPlacedDto>
    {
        public string MovieId { get; set; } = null!;

        public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderPlacedDto>
        {
            private readonly IOrderService _orderService;
            private readonly HttpContext _httpContext;

            public PlaceOrderCommandHandler(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
            {
                _orderService = orderService;
                _httpContext = httpContextAccessor.HttpContext;
            }

            public async Task<OrderPlacedDto> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
            {
                if (_httpContext.User.Identity is null || !_httpContext.User.Identity.IsAuthenticated)
                    throw new UnauthorizedAccessException();
                string userId = _httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
                                ?? throw new ArgumentNullException(ClaimTypes.NameIdentifier);
                return await _orderService.PlaceOrderAsync(new(userId, request.MovieId));
            }
        }
    }
}