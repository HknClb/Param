using FluentValidation;

namespace Application.Features.Stars.Commands.Delete
{
    public class DeleteStarCommandValidator : AbstractValidator<DeleteStarCommand>
    {
        public DeleteStarCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("The id is can't be null");
        }
    }
}