using FluentValidation;

namespace Application.Features.Movies.Queries.GetById
{
    public class GetMovieByIdQueryValidator : AbstractValidator<GetMovieByIdQuery>
    {
        public GetMovieByIdQueryValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("The id is can't be null");
        }
    }
}