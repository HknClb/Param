using FluentValidation;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        // => For more details go to Features > Products > Profiles > MappingProfiles.cs
        public UpdateProductCommandValidator()
        {
            // Is the product Id empty or null?
            RuleFor(command => command.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product id couldn't be null or empty");

            // Is the product name empty?
            // When we leave the product name field as null then we don't change the product name field
            RuleFor(command => command.Name)
                .NotEmpty()
                .When(command => command.Name is not null)
                .WithMessage("Product name couldn't be empty");

            // Is the product unit price greater than or equal to 0?
            // When we leave the product unit price field as null then we don't change the product unit price field
            RuleFor(command => command.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product unit price couldn't be less than 0");

            // Note: When we leave the product description field as empty then we will set the product description field null
            // Note: When we leave the product description field as null then we don't change the product description field
        }
    }
}
