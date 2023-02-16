using Domain.Entities;

namespace Application.Abstractions.Repositories.Products
{
    public interface IProductReadRepository : IReadRepository<Product>, IAsyncReadRepository<Product>
    {
    }
}
