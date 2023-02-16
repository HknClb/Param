using Application.Abstractions.Services;
using Application.Features.Products.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.OData
{
    public class ProductODataQuery : IRequest<IQueryable<ProductODataDto>>
    {
        public class ProductODataQueryHandler : IRequestHandler<ProductODataQuery, IQueryable<ProductODataDto>>
        {
            private readonly IProductService _productService;

            public ProductODataQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<IQueryable<ProductODataDto>> Handle(ProductODataQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Product> products = await _productService.GetListAsQueryableAsync();
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.CreateProjection<Product, ProductODataDto>();
                });
                IQueryable<ProductODataDto> productODataDto = products.ProjectTo<ProductODataDto>(configuration);
                return productODataDto;
            }
        }
    }
}
