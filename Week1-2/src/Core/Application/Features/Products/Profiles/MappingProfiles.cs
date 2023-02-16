using Application.Features.Products.Commands.Update;
using Application.Features.Products.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductGetByIdDto>().ReverseMap();
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, ProductCreatedDto>().ReverseMap();
            CreateMap<Product, ProductUpdatedDto>().ReverseMap();
            CreateMap<Product, ProductDeletedDto>().ReverseMap();

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.Condition(src => src.Name != null);
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.Description, opt =>
                {
                    opt.Condition(src => src.Description != null);
                    opt.MapFrom(src => src.Description == string.Empty ? null : src.Description);
                })
                .ForMember(dest => dest.UnitPrice, opt =>
                {
                    opt.Condition(src => src.UnitPrice != null);
                    opt.MapFrom(src => src.UnitPrice);
                })
                .ReverseMap();

            CreateMap<UpdateProductDto, Product>().ReverseMap();
        }
    }
}
