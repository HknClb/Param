using Application.Features.Users.Commands.Update;
using FluentValidation;

namespace Application.Features.Users.Commands.Create
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEqual(string.Empty)
                .WithMessage("Please enter user's name or leave null");
            RuleFor(command => command.Name)
                .MinimumLength(3)
                .When(name => name is not null)
                .WithMessage("The name length must be greater than 3");

            RuleFor(command => command.Surname)
                .NotEqual(string.Empty)
                .WithMessage("Please enter user's surname or leave null");
            RuleFor(command => command.Surname)
                .MinimumLength(3)
                .When(surname => surname is not null)
                .WithMessage("The surname length must be greater than 3");

            RuleFor(command => command.Email)
                .NotEqual(string.Empty)
                .WithMessage("Please enter user's email");
            RuleFor(command => command.Email).
                MinimumLength(3)
                .When(email => email is not null)
                .WithMessage("The surname length must be greater than 3");
            RuleFor(command => command.Email)
                .EmailAddress()
                .When(email => email is not null);

            RuleFor(command => command.UserName)
                .NotEqual(string.Empty)
                .WithMessage("Please enter user's username");
            RuleFor(command => command.UserName)
                .MinimumLength(3)
                .When(userName => userName is not null)
                .WithMessage("The surname length must be greater than 3");

            RuleFor(command => command.Password)
                .NotEqual(string.Empty)
                .WithMessage("Please enter user's password");
            RuleFor(command => command.Password)
                .MinimumLength(6)
                .When(password => password is not null)
                .WithMessage("The password length must be greater than 6");
            RuleFor(command => command.Password)
                .Must(password => password is null ? true : password.Any(x => char.IsUpper(x)))
                .WithMessage("The password must contain upper case");
            RuleFor(command => command.Password)
                .Must(password => password is null ? true : password.Any(x => char.IsUpper(x)))
                .WithMessage("The password must contain lower case");

            RuleFor(command => command.PhoneNumber)
                .NotEqual(string.Empty)
                .WithMessage("Phone number couldn't be empty");
        }
    }
}
