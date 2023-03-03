using Application.Abstractions.Services;
using Application.Features.Auth.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace Infrastructure.Services
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto CreateAccessToken(List<Claim> claims, TimeSpan expireTime)
        {
            TokenDto token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtBearer:SecurityKey"]!));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.Add(expireTime);
            JwtSecurityToken securityToken = new(
                    audience: _configuration["Authentication:JwtBearer:Audience"]!,
                    issuer: _configuration["Authentication:JwtBearer:Issuer"]!,
                    expires: token.Expiration,
                    notBefore: DateTime.UtcNow.AddSeconds(30),
                    signingCredentials: signingCredentials,
                    claims: claims
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();

            return token;
        }

        private string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
