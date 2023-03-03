using Application.Features.Auth.Dtos;

namespace Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<SignedInDto> SignInAsync(string userNameOrEmail, string password, TimeSpan accessTokenExpireTime);
        Task<SignedUpDto> SignUpAsync(SignUpDto signUpUser);
        Task<SignedInDto> RefreshTokenSignInAsync(string refreshToken, TimeSpan accessTokenExpireTime);
    }
}
