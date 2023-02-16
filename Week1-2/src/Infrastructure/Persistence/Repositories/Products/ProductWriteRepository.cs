using Application.Abstractions.Repositories.Products;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.Products
{
    public class ProductWriteRepository : EfWriteRepositoryBase<Product, BaseDbContext>, IProductWriteRepository
    {
        public ProductWriteRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
