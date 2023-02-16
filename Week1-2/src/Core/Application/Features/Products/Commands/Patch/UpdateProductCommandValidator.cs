using FluentValidation;

namespace Application.Features.Products.Commands.Patch
{
    public class UpdateProductByPatchCommandValidator : AbstractValidator<UpdateProductByPatchCommand>
    {
        // => For more details go to Features > Products > Profiles > MappingProfiles.cs
        public UpdateProductByPatchCommandValidator()
        {
            // Is the product Id empty or null?
            RuleFor(command => command.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product id couldn't be null or empty");
        }
    }
}
