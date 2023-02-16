using FluentValidation;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            // Is the product Id empty or null?
            RuleFor(command => command.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product id couldn't be null or empty");
        }
    }
}
