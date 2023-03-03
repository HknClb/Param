using CrossCuttingConcerns.Exceptions.Business;
using Domain.Entities;

namespace Application.Features.Orders.Rules
{
    public class OrderBusinessRules
    {
        public void MovieShouldBeExistWhenPlacingOrder(Movie? movie)
        {
            if (movie is null)
                throw new BusinessException("The movie you wanted to order could not be found");
        }

        public void OrderShouldntBeAlreadyExist(Order? order)
        {
            if (order is not null)
                throw new BusinessException("The movie has been already ordered");
        }
    }
}
