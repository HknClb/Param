using Domain.Entities;

namespace Application.Abstractions.Repositories.Products
{
    public interface IProductWriteRepository : IWriteRepository<Product>, IAsyncWriteRepository<Product>
    {
    }
}
