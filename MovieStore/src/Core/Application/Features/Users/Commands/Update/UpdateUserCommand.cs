using Application.Abstractions.Services;
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<UserUpdatedDto>
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? Surname { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; } = null!;
        public string[]? Roles { get; set; }
        public bool IsActive { get; set; } = true;

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserUpdatedDto>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userService = userService;
                _mapper = mapper;
            }

            public async Task<UserUpdatedDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
                => await _userService.UpdateAsync(_mapper.Map<UpdateUserDto>(request));
        }
    }
}