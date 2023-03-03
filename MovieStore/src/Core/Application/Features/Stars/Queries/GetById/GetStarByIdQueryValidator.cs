using FluentValidation;

namespace Application.Features.Stars.Queries.GetById
{
    public class GetStarByIdQueryValidator : AbstractValidator<GetStarByIdQuery>
    {
        public GetStarByIdQueryValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("The id is can't be null");
        }
    }
}