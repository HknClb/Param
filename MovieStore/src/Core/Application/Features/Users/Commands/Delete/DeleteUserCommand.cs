using Application.Abstractions.Services;
using Application.Features.Users.Dtos;
using MediatR;

namespace Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest<UserDeletedDto>
    {
        public string Id { get; set; } = null!;

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDeletedDto>
        {
            private readonly IUserService _userService;

            public DeleteUserCommandHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<UserDeletedDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                await _userService.DeleteAsync(request.Id);
                return new();
            }
        }
    }
}
