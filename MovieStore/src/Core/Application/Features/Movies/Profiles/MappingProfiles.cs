using Application.Abstractions.Paging;
using Application.Features.Movies.Commands.Create;
using Application.Features.Movies.Commands.Update;
using Application.Features.Movies.Dtos;
using Application.Features.Movies.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Movies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Director, MovieDirectorDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
                .ReverseMap();
            CreateMap<Star, MovieStarsListDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"))
                .ReverseMap();
            CreateMap<Genre, MovieGenresListDto>().ReverseMap();

            CreateMap<CreateMovieCommand, CreateMovieDto>().ReverseMap();
            CreateMap<CreateMovieDto, Movie>().ReverseMap();
            CreateMap<Movie, MovieCreatedDto>().ReverseMap();

            CreateMap<UpdateMovieCommand, UpdateMovieDto>()
                .ForMember(dest => dest.GenreIds, opt =>
                {
                    opt.Condition(src => src.GenreIds is not null);
                    opt.MapFrom(src => src.GenreIds);
                })
                .ForMember(dest => dest.StarIds, opt =>
                {
                    opt.Condition(src => src.StarIds is not null);
                    opt.MapFrom(src => src.StarIds);
                })
                .ReverseMap();
            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.Condition(src => src.Name is not null);
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.Price, opt =>
                {
                    opt.Condition(src => src.Price is not null);
                    opt.MapFrom(src => src.Price);
                })
                .ForMember(dest => dest.PublishedYear, opt =>
                {
                    opt.Condition(src => src.PublishedYear is not null);
                    opt.MapFrom(src => src.PublishedYear);
                })
                .ForMember(dest => dest.DirectorId, opt =>
                {
                    opt.Condition(src => src.DirectorId is not null);
                    opt.MapFrom(src => src.DirectorId);
                })
                .ReverseMap();
            CreateMap<Movie, MovieUpdatedDto>().ReverseMap();

            CreateMap<Movie, MovieGetByIdDto>().ReverseMap();

            CreateMap<Movie, MoviesListDto>()
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
                .ForMember(dest => dest.Price, opt =>
                {
                    opt.Condition(src => src.Price != default);
                    opt.MapFrom(src => src.Price);
                })
                .ForMember(dest => dest.PublishedYear, opt =>
                {
                    opt.Condition(src => src.PublishedYear != default);
                    opt.MapFrom(src => src.PublishedYear);
                })
                .ForMember(dest => dest.Director, opt =>
                {
                    opt.Condition(src => src.Director != default);
                    opt.MapFrom(src => src.Director);
                })
                .ForMember(dest => dest.Stars, opt =>
                {
                    opt.Condition(src => src.Stars.Any());
                    opt.MapFrom(src => src.Stars);
                })
                .ForMember(dest => dest.Genres, opt =>
                {
                    opt.Condition(src => src.Genres.Any());
                    opt.MapFrom(src => src.Genres);
                })
                .ForMember(dest => dest.CreatedDate, opt =>
                {
                    opt.Condition(src => src.CreatedDate != default);
                    opt.MapFrom(src => src.CreatedDate);
                })
                .ForMember(dest => dest.UpdatedDate, opt =>
                {
                    opt.Condition(src => src.UpdatedDate != default);
                    opt.MapFrom(src => src.UpdatedDate);
                })
                .ReverseMap();
            CreateMap<IPaginate<Movie>, MoviesListModel>().ReverseMap();
        }
    }
}