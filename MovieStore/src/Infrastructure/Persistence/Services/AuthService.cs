using Application.Abstractions.Services;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Features.Users.Dtos;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler, IUserService userService,
            AuthBusinessRules authBusinessRules)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
        }

        private async Task<TokenDto> GenerateTokenAsync(User user, TimeSpan accessTokenExpireTime)
        {
            TokenDto token = _tokenHandler.CreateAccessToken(await PrepareUserClaimsAsync(user), accessTokenExpireTime);
            await _userService.UpdateRefreshTokenAsync(user!, token, TimeSpan.FromDays(30));
            return token;
        }

        public async Task<SignedInDto> SignInAsync(string userNameOrEmail, string password, TimeSpan accessTokenExpireTime)
        {
            User? user = (await _userManager.FindByNameAsync(userNameOrEmail)) ?? (await _userManager.FindByEmailAsync(userNameOrEmail));
            _authBusinessRules.UserShouldBeFoundWhenSignIn(user);
            await _authBusinessRules.CustomerShouldBeActiveWhenSignInAsync(user!);
            _authBusinessRules.UserShouldBeSignedIn(await _signInManager.PasswordSignInAsync(user!, password, false, false));

            TokenDto token = await GenerateTokenAsync(user!, accessTokenExpireTime);
            return new(token);
        }

        public async Task<SignedUpDto> SignUpAsync(SignUpDto signUpUser)
        {
            await _authBusinessRules.EmailShouldBeUniqueWhenSignUpAsync(signUpUser.Email);
            UserCreatedDto user = await _userService.CreateAsync(
                new(signUpUser.Name, signUpUser.Surname, signUpUser.UserName, signUpUser.Email, signUpUser.Password, null, new[] { "customer" }));
            return new();
        }

        public async Task<SignedInDto> RefreshTokenSignInAsync(string refreshToken, TimeSpan accessTokenExpireTime)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            _authBusinessRules.UserShouldBeFoundWhenSignIn(user);
            await _authBusinessRules.CustomerShouldBeActiveWhenSignInAsync(user!);
            _authBusinessRules.RefreshTokenShouldntBeExpiredWhenSignIn(user!.RefreshTokenExpiration);

            TokenDto token = await GenerateTokenAsync(user, accessTokenExpireTime);
            return new(token);
        }

        private async Task<List<Claim>> PrepareUserClaimsAsync(User user)
        {
            List<Claim> claims = new()
            {
                new(ClaimTypes.NameIdentifier, user!.Id),
                new(ClaimTypes.Name, user.UserName ?? "?")
            };

            IList<string>? roles = await _userManager.GetRolesAsync(user);
            if (roles is not null)
                foreach (var role in roles)
                    claims.Add(new(ClaimTypes.Role, role));

            return claims;
        }
    }
}