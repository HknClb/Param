using FluentValidation;

namespace Application.Features.Orders.Queries.OrdersListByUserId
{
    public class GetOrdersListByUserIdQueryValidator : AbstractValidator<GetOrdersListByUserIdQuery>
    {
        public GetOrdersListByUserIdQueryValidator()
        {
            RuleFor(query => query.UserId)
                .NotEmpty()
                .WithMessage("User Id is required");
        }
    }
}
