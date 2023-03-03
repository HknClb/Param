using Application.Abstractions.Paging;
using Application.Features.Directors.Commands.Create;
using Application.Features.Directors.Commands.Update;
using Application.Features.Directors.Dtos;
using Application.Features.Directors.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Directors.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, MoviesDirectedListDto>().ReverseMap();

            CreateMap<CreateDirectorCommand, CreateDirectorDto>().ReverseMap();
            CreateMap<Director, DirectorCreatedDto>().ReverseMap();
            CreateMap<CreateDirectorDto, Director>().ReverseMap();

            CreateMap<UpdateDirectorCommand, UpdateDirectorDto>().ReverseMap();
            CreateMap<Director, DirectorUpdatedDto>()
                .ForMember(dest => dest.MoviesDirected, opt => opt.MapFrom(x => x.Movies))
                .ReverseMap();
            CreateMap<UpdateDirectorDto, Director>()
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

            CreateMap<Director, DirectorGetByIdDto>()
                .ForMember(dest => dest.MoviesDirected, opt => opt.MapFrom(src => src.Movies))
                .ReverseMap();

            CreateMap<Director, DirectorsListDto>()
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
                .ForMember(dest => dest.MoviesDirected, opt => opt.MapFrom(src => src.Movies))
                .ReverseMap();
            CreateMap<IPaginate<Director>, DirectorsListModel>().ReverseMap();
        }
    }
}
