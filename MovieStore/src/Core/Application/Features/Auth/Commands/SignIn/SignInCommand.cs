using Application.Abstractions.Services;
using Application.Features.Auth.Dtos;
using MediatR;

namespace Application.Features.Auth.Commands.SignIn
{
    public class SignInCommand : IRequest<SignedInDto>
    {
        public string UserNameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;

        public class SignInCommandHandler : IRequestHandler<SignInCommand, SignedInDto>
        {
            private readonly IAuthService _authService;

            public SignInCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<SignedInDto> Handle(SignInCommand request, CancellationToken cancellationToken)
                => await _authService.SignInAsync(request.UserNameOrEmail, request.Password, TimeSpan.FromDays(1));
        }
    }
}
