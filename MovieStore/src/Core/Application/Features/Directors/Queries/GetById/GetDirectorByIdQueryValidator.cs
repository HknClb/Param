using FluentValidation;

namespace Application.Features.Directors.Queries.GetById
{
    public class GetDirectorByIdQueryValidator : AbstractValidator<GetDirectorByIdQuery>
    {
        public GetDirectorByIdQueryValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("The id is can't be null");
        }
    }
}