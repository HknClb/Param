using Application.Abstractions.Repositories.Products;
using Application.Abstractions.Services;
using Application.DynamicQuery;
using Domain.Entities;

namespace Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<Product> AddProductAsync(Product product)
            => await _productWriteRepository.AddAsync(product);

        public async Task<Product> UpdateProductAsync(Product product)
            => await _productWriteRepository.UpdateAsync(product);

        public async Task<Product?> DeleteProductAsync(string id)
            => await _productWriteRepository.DeleteByIdAsync(id);

        public async Task<Product?> GetByIdAsync(string id)
            => await _productReadRepository.GetAsync(x => x.Id == Guid.Parse(id));

        public async Task<IList<Product>> GetListAsync(Dynamic dynamic)
            => await _productReadRepository.GetListByDynamicAsync(dynamic, enableTracking: false);

        public async Task<IQueryable<Product>> GetListAsQueryableAsync()
            => await _productReadRepository.GetListAsQueryableAsync();
    }
}
