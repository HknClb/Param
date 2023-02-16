using Application.Abstractions.Services;
using Application.Features.Products.Dtos;
using Application.Features.Products.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<ProductCreatedDto>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductCreatedDto>
        {
            private readonly IProductService _productService;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly IMapper _mapper;

            public CreateProductCommandHandler(IProductService productService, ProductBusinessRules productBusinessRules, IMapper mapper)
            {
                _productService = productService;
                _productBusinessRules = productBusinessRules;
                _mapper = mapper;
            }

            public async Task<ProductCreatedDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                Product product = await _productService.AddProductAsync(new(request.Name, request.UnitPrice, request.Description));
                ProductCreatedDto createdProduct = _mapper.Map<ProductCreatedDto>(product);
                return createdProduct;
            }
        }
    }
}
