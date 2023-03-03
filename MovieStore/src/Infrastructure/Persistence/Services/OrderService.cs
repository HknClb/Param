using Application.Abstractions.Paging;
using Application.Abstractions.Services;
using Application.Abstractions.UnitOfWorks.Base;
using Application.DynamicQuery;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Rules;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrderBusinessRules _orderBusinessRules;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, OrderBusinessRules orderBusinessRules, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderBusinessRules = orderBusinessRules;
            _mapper = mapper;
        }

        public async Task<OrdersListModel> GetOrdersByUserId(string userId, Dynamic dynamic, PageRequest pageRequest, CancellationToken cancellationToken = default)
        {
            IPaginate<Order> orders = await _unitOfWork.ReadRepository<Order>()
                .GetListByDynamicAsPaginateAsync(dynamic, x => x.UserId == userId, x => x.Include(x => x.Movie), pageRequest.Page, pageRequest.PageSize, false, cancellationToken);

            return _mapper.Map<OrdersListModel>(orders);
        }

        public async Task<OrderPlacedDto> PlaceOrderAsync(PlaceOrderDto placeOrder)
        {
            Movie? movie = await _unitOfWork.ReadRepository<Movie>().GetAsync(x => x.Id == Guid.Parse(placeOrder.MovieId) && x.IsActive, enableTracking: false);
            _orderBusinessRules.MovieShouldBeExistWhenPlacingOrder(movie);
            Order? order = await _unitOfWork.ReadRepository<Order>().GetAsync(x => x.UserId == placeOrder.UserId && x.MovieId == movie!.Id);
            _orderBusinessRules.OrderShouldntBeAlreadyExist(order);

            order = await _unitOfWork.WriteRepository<Order>().AddAsync(new(placeOrder.UserId, movie!.Id, movie.Price));
            await _unitOfWork.CompleteAsync();

            return new() { MovieName = movie.Name, PublishedYear = movie.PublishedYear, Price = order.Price, OrderedDate = order.CreatedDate };
        }
    }
}
