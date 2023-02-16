using Application.Abstractions.Services;
using Application.DynamicQuery;
using Application.Features.Products.Dtos;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.List
{
    public class ProductListQuery : IRequest<IList<ProductListDto>>
    {
        public Dynamic? Dynamic { get; set; }

        public class ProductListQueryHandler : IRequestHandler<ProductListQuery, IList<ProductListDto>>
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;

            public ProductListQueryHandler(IProductService productService, IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }

            public async Task<IList<ProductListDto>> Handle(ProductListQuery request, CancellationToken cancellationToken)
            {
                IList<Product> products = await _productService.GetListAsync(request.Dynamic ?? new());
                List<ProductListDto> productListDto = _mapper.Map<List<ProductListDto>>(products);
                return productListDto;
            }
        }
    }
}
