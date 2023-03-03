using FluentValidation;

namespace Application.Features.Movies.Commands.Create
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(command => command.DirectorId)
                .NotEmpty()
                .WithMessage("The movie director is required");

            RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage("The movie name is required");
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
                .NotEmpty()
                .Must(genreIds => genreIds?.Length > 0)
                .WithMessage("The movie must has a genre");
            RuleForEach(command => command.GenreIds)
                .NotEmpty()
                .WithMessage("The genre id can't be empty");

            RuleFor(command => command.StarIds)
                .NotEmpty()
                .Must(starIds => starIds?.Length >= 3)
                .WithMessage("The movie must has stars");
            RuleForEach(command => command.StarIds)
                .NotEmpty()
                .WithMessage("The star id can't be empty");
        }
    }
}
