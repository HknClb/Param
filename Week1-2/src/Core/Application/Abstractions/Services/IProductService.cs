using Application.DynamicQuery;
using Domain.Entities;

namespace Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<Product?> GetByIdAsync(string id);
        Task<IList<Product>> GetListAsync(Dynamic dynamic);
        Task<IQueryable<Product>> GetListAsQueryableAsync();
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<Product?> DeleteProductAsync(string id);
    }
}
