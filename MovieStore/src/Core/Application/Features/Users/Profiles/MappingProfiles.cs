using Application.Abstractions.Paging;
using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using AutoMapper;
using Domain.Entities.Identity;

namespace Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserCreatedDto>()
                .ForMember(dest => dest.Roles, opt =>
                {
                    opt.Condition(src => src.Roles.Any());
                    opt.MapFrom(src => src.Roles.Select(x => x.Name).ToArray());
                })
                .ReverseMap();
            CreateMap<User, UserGetByIdDto>()
                .ForMember(dest => dest.Roles, opt =>
                {
                    opt.Condition(src => src.Roles.Any());
                    opt.MapFrom(src => src.Roles.Select(x => x.Name).ToArray());
                })
                .ReverseMap();
            CreateMap<User, UsersListDto>()
                .ForMember(dest => dest.Roles, opt =>
                 {
                     opt.Condition(src => src.Roles.Any());
                     opt.MapFrom(src => src.Roles.Select(x => x.Name).ToArray());
                 })
                .ReverseMap();
            CreateMap<User, UserUpdatedDto>()
                .ForMember(dest => dest.Roles, opt =>
                {
                    opt.Condition(src => src.Roles.Any());
                    opt.MapFrom(src => src.Roles.Select(x => x.Name).ToArray());
                })
                .ReverseMap();

            CreateMap<IPaginate<User>, UsersListModel>().ReverseMap();

            CreateMap<CreateUserCommand, CreateUserDto>().ReverseMap();

            CreateMap<UpdateUserCommand, UpdateUserDto>().ReverseMap();
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
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
                .ForMember(dest => dest.Email, opt =>
                {
                    opt.Condition(src => src.Email is not null);
                    opt.MapFrom(src => src.Email);
                })
                .ForMember(dest => dest.UserName, opt =>
                {
                    opt.Condition(src => src.UserName is not null);
                    opt.MapFrom(src => src.UserName);
                })
                .ForMember(dest => dest.PhoneNumber, opt =>
                {
                    opt.Condition(src => src.PhoneNumber is not null);
                    opt.MapFrom(src => src.PhoneNumber);
                })
                .ReverseMap();
        }
    }
}
