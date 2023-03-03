using Application.Features.Auth.Dtos;
using System.Security.Claims;

namespace Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(List<Claim> claims, TimeSpan expireTime);
    }
}
