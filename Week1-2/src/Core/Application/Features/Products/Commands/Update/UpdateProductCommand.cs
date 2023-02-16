using Application.Abstractions.Services;
using Application.Features.Products.Dtos;
using Application.Features.Products.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<ProductUpdatedDto>
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool IsActive { get; set; } = true;

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductUpdatedDto>
        {
            private readonly IProductService _productService;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IProductService productService, ProductBusinessRules productBusinessRules, IMapper mapper)
            {
                _productService = productService;
                _productBusinessRules = productBusinessRules;
                _mapper = mapper;
            }

            public async Task<ProductUpdatedDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                Product? product = await _productService.GetByIdAsync(request.Id!);
                _productBusinessRules.ProductShouldBeExist(product);
                product = _mapper.Map(request, product);
                product = await _productService.UpdateProductAsync(product!);
                ProductUpdatedDto productUpdatedDto = _mapper.Map<ProductUpdatedDto>(product);
                return productUpdatedDto;
            }
        }
    }
}
