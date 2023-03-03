using FluentValidation;

namespace Application.Features.Auth.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(command => command.UserNameOrEmail).NotNull().NotEmpty().WithMessage("Please enter your email or username");
            RuleFor(command => command.Password).NotNull().NotEmpty().WithMessage("Please enter your password");
        }
    }
}
