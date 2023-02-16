using Application.Abstractions.Repositories.Products;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.Products
{
    public class ProductReadRepository : EfReadRepositoryBase<Product, BaseDbContext>, IProductReadRepository
    {
        public ProductReadRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
