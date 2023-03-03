using FluentValidation;

namespace Application.Features.Directors.Commands.Update
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("The id is can't be null");

            RuleFor(command => command.Name)
                .NotEqual(string.Empty)
                .WithMessage("The name of star can't be empty. Enter new name or leave as null");
            RuleFor(command => command.Name)
                .Length(3, 50)
                .WithMessage("The name of star length should be less than 3 and larger than 50");

            RuleFor(command => command.Surname)
                .NotEqual(string.Empty)
                .WithMessage("The surname of star can't be empty. Enter new surname or leave as null");
            RuleFor(command => command.Surname)
                .Length(3, 50)
                .WithMessage("The surname of star length should be less than 3 and larger than 50");
        }
    }
}