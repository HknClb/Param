using FluentValidation;

namespace Application.Features.Auth.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(command => command.Name).NotNull().NotEmpty().WithMessage("Please enter your name");
            RuleFor(command => command.Name).MinimumLength(3).WithMessage("The name length must be greater than 3");

            RuleFor(command => command.Surname).NotNull().NotEmpty().WithMessage("Please enter your surname");
            RuleFor(command => command.Surname).MinimumLength(3).WithMessage("The surname length must be greater than 3");

            RuleFor(command => command.Email).NotNull().NotEmpty().WithMessage("Please enter your email");
            RuleFor(command => command.Email).MinimumLength(3).WithMessage("The surname length must be greater than 3");
            RuleFor(command => command.Email).EmailAddress();

            RuleFor(command => command.UserName).NotNull().NotEmpty().WithMessage("Please enter your username");
            RuleFor(command => command.UserName).MinimumLength(3).WithMessage("The surname length must be greater than 3");

            RuleFor(command => command.Password).NotNull().NotEmpty().WithMessage("Please enter your password");
            RuleFor(command => command.Password).MinimumLength(6).WithMessage("The password length must be greater than 6");
            RuleFor(command => command.Password).Matches(command => command.ConfirmPassword).WithMessage("Passwords are not match");
            RuleFor(command => command.Password).Must(password => password.Any(x => char.IsUpper(x))).WithMessage("The password must contain upper case");
            RuleFor(command => command.Password).Must(password => password.Any(x => char.IsLower(x))).WithMessage("The password must contain lower case");
        }
    }
}
