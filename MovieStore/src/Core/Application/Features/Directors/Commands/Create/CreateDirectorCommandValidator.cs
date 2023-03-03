using FluentValidation;

namespace Application.Features.Directors.Commands.Create
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage("The name of star can't be empty");
            RuleFor(command => command.Name)
                .Length(3, 50)
                .WithMessage("The name of star length should be less than 3 and larger than 50");

            RuleFor(command => command.Surname)
                .NotEmpty()
                .WithMessage("The surname of star can't be empty");
            RuleFor(command => command.Surname)
                .Length(3, 50)
                .WithMessage("The surname of star length should be less than 3 and larger than 50");
        }
    }
}
