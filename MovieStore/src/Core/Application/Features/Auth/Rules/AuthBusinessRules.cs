using CrossCuttingConcerns.Exceptions.Business;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Rules
{
    public class AuthBusinessRules
    {
        private readonly UserManager<User> _userManager;

        public AuthBusinessRules(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public void UserShouldBeFoundWhenSignIn(User? user)
        {
            if (user is null)
                throw new BusinessException("The email address, username or password is incorrect.");
        }

        public void UserShouldBeSignedIn(SignInResult signInResult)
        {
            if (!signInResult.Succeeded)
                throw new BusinessException("The email address, username or password is incorrect.");
        }

        public void RefreshTokenShouldntBeExpiredWhenSignIn(DateTime? refreshTokenExpiration)
        {
            if (refreshTokenExpiration is null)
                throw new BusinessException("Refresh token not found");
            if (refreshTokenExpiration < DateTime.UtcNow)
                throw new BusinessException("Refresh Token expired");
        }

        public async Task EmailShouldBeUniqueWhenSignUpAsync(string email)
        {
            User? user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
                throw new BusinessException("Email already exist");
        }

        public async Task CustomerShouldBeActiveWhenSignInAsync(User user)
        {
            if (await _userManager.IsInRoleAsync(user, "customer") && !user.IsActive)
                throw new UnauthorizedAccessException();
        }
    }
}
