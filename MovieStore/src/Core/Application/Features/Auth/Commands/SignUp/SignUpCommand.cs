using Application.Abstractions.Services;
using Application.Features.Auth.Dtos;
using MediatR;

namespace Application.Features.Auth.Commands.SignUp
{
    public class SignUpCommand : IRequest<SignedUpDto>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;

        public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignedUpDto>
        {
            private readonly IAuthService _authService;

            public SignUpCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<SignedUpDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
                => await _authService.SignUpAsync(new(request.Name, request.Surname, request.Email, request.UserName, request.Password));
        }
    }
}
