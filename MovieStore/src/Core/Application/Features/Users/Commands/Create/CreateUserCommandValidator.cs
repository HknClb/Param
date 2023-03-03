using FluentValidation;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Name).NotNull().NotEmpty().WithMessage("Please enter user's name");
            RuleFor(command => command.Name).MinimumLength(3).WithMessage("The name length must be greater than 3");

            RuleFor(command => command.Surname).NotNull().NotEmpty().WithMessage("Please enter user's surname");
            RuleFor(command => command.Surname).MinimumLength(3).WithMessage("The surname length must be greater than 3");

            RuleFor(command => command.Email).NotNull().NotEmpty().WithMessage("Please enter user's email");
            RuleFor(command => command.Email).MinimumLength(3).WithMessage("The surname length must be greater than 3");
            RuleFor(command => command.Email).EmailAddress();

            RuleFor(command => command.UserName).NotNull().NotEmpty().WithMessage("Please enter user's username");
            RuleFor(command => command.UserName).MinimumLength(3).WithMessage("The surname length must be greater than 3");

            RuleFor(command => command.Password).NotNull().NotEmpty().WithMessage("Please enter user's password");
            RuleFor(command => command.Password).MinimumLength(6).WithMessage("The password length must be greater than 6");
            RuleFor(command => command.Password).Must(password => password.Any(x => char.IsUpper(x))).WithMessage("The password must contain upper case");
            RuleFor(command => command.Password).Must(password => password.Any(x => char.IsLower(x))).WithMessage("The password must contain lower case");

            RuleFor(command => command.PhoneNumber).NotEqual(string.Empty).WithMessage("Phone number couldn't be empty");
        }
    }
}
