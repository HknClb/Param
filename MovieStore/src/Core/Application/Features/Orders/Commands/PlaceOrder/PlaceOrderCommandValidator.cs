using FluentValidation;

namespace Application.Features.Orders.Commands.PlaceOrder
{
    public class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderCommandValidator()
        {
            RuleFor(command => command.MovieId)
                .NotEmpty()
                .WithMessage("The Movie Id is required");
        }
    }
}
