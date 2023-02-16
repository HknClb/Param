using Application.Abstractions.Services;
using Application.Features.Products.Dtos;
using Application.Features.Products.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Features.Products.Commands.Patch
{
    public class UpdateProductByPatchCommand : IRequest<ProductUpdatedDto>
    {
        public string? Id { get; set; }
        public JsonPatchDocument<UpdateProductDto> ProductPatchDocument { get; set; } = null!;

        public class UpdateProductByPatchCommandHandler : IRequestHandler<UpdateProductByPatchCommand, ProductUpdatedDto>
        {
            private readonly IProductService _productService;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly IMapper _mapper;

            public UpdateProductByPatchCommandHandler(IProductService productService, ProductBusinessRules productBusinessRules, IMapper mapper)
            {
                _productService = productService;
                _productBusinessRules = productBusinessRules;
                _mapper = mapper;
            }

            public async Task<ProductUpdatedDto> Handle(UpdateProductByPatchCommand request, CancellationToken cancellationToken)
            {
                Product? product = await _productService.GetByIdAsync(request.Id!);
                _productBusinessRules.ProductShouldBeExist(product);

                UpdateProductDto updateProductDto = _mapper.Map<UpdateProductDto>(product);
                request.ProductPatchDocument.ApplyTo(updateProductDto);

                product = _mapper.Map(updateProductDto, product);
                product = await _productService.UpdateProductAsync(product!);
                return _mapper.Map<ProductUpdatedDto>(product);
            }
        }
    }
}
