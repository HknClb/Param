using FluentValidation;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            // Is the product name empty or null?
            RuleFor(command => command.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product name couldn't be empty or null");
            // Is the product name length greater than or equal to 3?
            RuleFor(command => command.Name).MinimumLength(3).WithMessage("Product name length should be greater than or equal to 3");

            // Is the product unit price greater than or equal to 0?
            RuleFor(command => command.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product unit price couldn't be less than 0");

            // Is the product description empty when not null?
            RuleFor(command => command.Description)
                .NotEmpty()
                .When(command => command.Description is not null)
                .WithMessage("Product description couldn't be empty. Leave without give value or fill this field.");
        }
    }
}
