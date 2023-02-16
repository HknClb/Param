using FluentValidation;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            // Is the product ID empty or null?
            RuleFor(query => query.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product id couldn't be null or empty");

            // Is the product ID in a correct format?
            RuleFor(query => query.Id)
                .Matches(@"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$")
                .WithMessage("Product id is not in a correct format");

            //RuleFor(query => query.Id) => Second way for Is the product ID in a correct format?
            //    .Must(id => Regex.IsMatch(id, @"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$"))
            //    .WithMessage("Product id is not in a correct format");
        }
    }
}
