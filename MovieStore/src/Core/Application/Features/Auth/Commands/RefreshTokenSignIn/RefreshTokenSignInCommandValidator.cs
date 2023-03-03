using FluentValidation;

namespace Application.Features.Auth.Commands.SignInWithRefreshToken
{
    public class RefreshTokenSignInCommandValidator : AbstractValidator<RefreshTokenSignInCommand>
    {
        public RefreshTokenSignInCommandValidator()
        {
            RuleFor(command => command.RefreshToken).NotNull().NotEmpty().WithMessage("Please enter your refresh token");
        }
    }
}