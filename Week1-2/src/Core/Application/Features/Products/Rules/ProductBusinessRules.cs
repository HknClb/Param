using CrossCuttingConcerns.Exceptions.Business;
using Domain.Entities;

namespace Application.Features.Products.Rules
{
    public class ProductBusinessRules
    {
        public void ProductShouldBeExist(Product? product)
        {
            if (product is null)
                throw new BusinessException("Product is not found");
        }

        public void ProductShouldBeExistWhenDeleting(Product? product)
        {
            if (product is null)
                throw new BusinessException("Product is not found or couldn't deleted");
        }
    }
}
