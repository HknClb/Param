using Application.Abstractions.Paging;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Orders.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Order, OrdersListDto>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Name))
                .ForMember(dest => dest.OrderedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.PublishedYear, opt => opt.MapFrom(src => src.Movie.PublishedYear))
                .ReverseMap();
            CreateMap<IPaginate<Order>, OrdersListModel>().ReverseMap();
        }
    }
}