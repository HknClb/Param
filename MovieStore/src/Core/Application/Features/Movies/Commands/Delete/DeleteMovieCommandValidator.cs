using FluentValidation;

namespace Application.Features.Movies.Commands.Delete
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("The id is can't be null");
        }
    }
}