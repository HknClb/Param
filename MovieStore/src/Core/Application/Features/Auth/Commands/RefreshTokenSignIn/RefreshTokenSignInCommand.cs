using Application.Abstractions.Services;
using Application.Features.Auth.Dtos;
using MediatR;

namespace Application.Features.Auth.Commands.SignInWithRefreshToken
{
    public class RefreshTokenSignInCommand : IRequest<SignedInDto>
    {
        public string RefreshToken { get; set; } = null!;

        public class RefreshTokenSignInCommandHandler : IRequestHandler<RefreshTokenSignInCommand, SignedInDto>
        {
            private readonly IAuthService _authService;

            public RefreshTokenSignInCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public Task<SignedInDto> Handle(RefreshTokenSignInCommand request, CancellationToken cancellationToken)
                => _authService.RefreshTokenSignInAsync(request.RefreshToken, TimeSpan.FromDays(1));
        }
    }
}
