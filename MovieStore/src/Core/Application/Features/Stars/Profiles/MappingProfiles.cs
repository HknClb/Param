using Application.Abstractions.Paging;
using Application.Features.Stars.Commands.Create;
using Application.Features.Stars.Commands.Update;
using Application.Features.Stars.Dtos;
using Application.Features.Stars.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Stars.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, StarringMoviesListDto>().ReverseMap();

            CreateMap<CreateStarCommand, CreateStarDto>().ReverseMap();
            CreateMap<Star, StarCreatedDto>().ReverseMap();
            CreateMap<CreateStarDto, Star>().ReverseMap();

            CreateMap<UpdateStarCommand, UpdateStarDto>().ReverseMap();
            CreateMap<Star, StarUpdatedDto>()
                .ForMember(dest => dest.StarringMovies, opt => opt.MapFrom(x => x.Movies))
                .ReverseMap();
            CreateMap<UpdateStarDto, Star>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.Condition(src => src.Name is not null);
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.Surname, opt =>
                {
                    opt.Condition(src => src.Surname is not null);
                    opt.MapFrom(src => src.Surname);
                })
                .ReverseMap();

            CreateMap<Star, StarGetByIdDto>()
                .ForMember(dest => dest.StarringMovies, opt => opt.MapFrom(src => src.Movies))
                .ReverseMap();

            CreateMap<Star, StarsListDto>()
                .ForMember(dest => dest.Id, opt =>
                {
                    opt.Condition(src => src.Id != default);
                    opt.MapFrom(src => src.Id);
                })
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.Condition(src => src.Name != default);
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.Surname, opt =>
                {
                    opt.Condition(src => src.Surname != default);
                    opt.MapFrom(src => src.Surname);
                })
                .ForMember(dest => dest.CreatedDate, opt =>
                {
                    opt.Condition(src => src.CreatedDate != default);
                    opt.MapFrom(src => src.CreatedDate);
                })
                .ForMember(dest => dest.StarringMovies, opt => opt.MapFrom(src => src.Movies))
                .ReverseMap();
            CreateMap<IPaginate<Star>, StarsListModel>().ReverseMap();
        }
    }
}