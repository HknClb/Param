using Application.Abstractions.Services;
using Application.Features.Users.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<UserCreatedDto>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = null!;
        public string[]? Roles { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserCreatedDto>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IUserService userService, IMapper mapper)
            {
                _userService = userService;
                _mapper = mapper;
            }

            public async Task<UserCreatedDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                UserCreatedDto userCreatedDto = await _userService.CreateAsync(_mapper.Map<CreateUserDto>(request));
                return userCreatedDto;
            }
        }
    }
}