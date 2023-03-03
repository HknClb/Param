using FluentValidation;

namespace Application.Features.Directors.Commands.Delete
{
    public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("The id is can't be null");
        }
    }
}