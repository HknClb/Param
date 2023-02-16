using Application.Abstractions.Services;
using Application.Features.Products.Dtos;
using Application.Features.Products.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQuery : IRequest<ProductGetByIdDto>
    {
        public string Id { get; set; } = null!;

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductGetByIdDto>
        {
            private readonly IProductService _productService;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IProductService productService, ProductBusinessRules productBusinessRules, IMapper mapper)
            {
                _productService = productService;
                _productBusinessRules = productBusinessRules;
                _mapper = mapper;
            }

            public async Task<ProductGetByIdDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                Product? product = await _productService.GetByIdAsync(request.Id);
                _productBusinessRules.ProductShouldBeExist(product);
                ProductGetByIdDto productGetByIdDto = _mapper.Map<ProductGetByIdDto>(product);
                return productGetByIdDto;
            }
        }
    }
}
