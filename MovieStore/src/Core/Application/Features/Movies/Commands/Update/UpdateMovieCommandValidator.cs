using FluentValidation;

namespace Application.Features.Movies.Commands.Update
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(command => command.DirectorId)
               .NotEqual(string.Empty)
               .WithMessage("The movie director can't be empty. Enter new director id or leave as null");

            RuleFor(command => command.Name)
                .NotEqual(string.Empty)
                .WithMessage("The movie name can't be empty. Enter new name or leave as null");
            RuleFor(command => command.Name)
                .Length(3, 250)
                .WithMessage("The movie name length should be larger than 3 and less than 250");

            RuleFor(command => command.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The movie price is can't be less than 0");

            RuleFor(command => command.PublishedYear)
                .LessThanOrEqualTo(DateTime.UtcNow.Year)
                .GreaterThanOrEqualTo(1888)
                .WithMessage($"The movie published year should be between {DateTime.UtcNow.Year} - 1888 years");

            RuleFor(command => command.GenreIds)
                .NotEqual(Array.Empty<int>())
                .WithMessage("The movie genres can't be empty. Enter genres or leave as null")
                .DependentRules(() =>
                {
                    RuleForEach(command => command.GenreIds)
                        .NotEmpty()
                        .WithMessage("The genre id can't be empty");
                });

            RuleFor(command => command.StarIds)
                .NotEqual(Array.Empty<string>())
                .WithMessage("The movie stars can't be empty. Enter stars or leave as null")
                .DependentRules(() =>
                {
                    RuleForEach(command => command.StarIds)
                        .NotEmpty()
                        .WithMessage("The star id can't be empty");
                });
            RuleFor(command => command.StarIds)
                .Must(starIds => starIds is null || starIds.Length >= 3)
                .WithMessage("The movie must has min 3 stars. Enter min 3 star or leave as null");
        }
    }
}