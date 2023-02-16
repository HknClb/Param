using Application.Abstractions.Services;
using Application.Features.Products.Dtos;
using Application.Features.Products.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<ProductDeletedDto>
    {
        public string Id { get; set; } = null!;

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductDeletedDto>
        {
            private readonly IProductService _productService;
            private readonly ProductBusinessRules _productBusinessRules;
            private readonly IMapper _mapper;

            public DeleteProductCommandHandler(IProductService productService, ProductBusinessRules productBusinessRules, IMapper mapper)
            {
                _productService = productService;
                _productBusinessRules = productBusinessRules;
                _mapper = mapper;
            }

            public async Task<ProductDeletedDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                Product? deletedProduct = await _productService.DeleteProductAsync(request.Id);
                _productBusinessRules.ProductShouldBeExistWhenDeleting(deletedProduct);
                return _mapper.Map<ProductDeletedDto>(deletedProduct);
            }
        }
    }
}
