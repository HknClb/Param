using Application.Abstractions.UnitOfWorks.Base;
using Application.Features.Users.Dtos;
using CrossCuttingConcerns.Exceptions.Business;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserBusinessRules(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public void UserShouldBeExist(User? user)
        {
            if (user is null)
                throw new BusinessException("User not exist");
        }

        public void UserShouldBeExistWhenGetById(UserGetByIdDto? user)
        {
            if (user is null)
                throw new BusinessException("User not exist");
        }

        public void UserShouldBeCreated(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BusinessException($"User couldn't created\n {PrepareErrorMessageFromIdentityResult(result)}");
        }

        public void UserShouldBeUpdated(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BusinessException($"User couldn't updated\n {PrepareErrorMessageFromIdentityResult(result)}");
        }

        public void UserPasswordShouldBeChangedWhenUpdating(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BusinessException($"User password couldn't updated\n {PrepareErrorMessageFromIdentityResult(result)}");
        }

        public void RoleAssignmentToUserShouldBeSuccessful(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BusinessException($"User role couldn't assigned\n {PrepareErrorMessageFromIdentityResult(result)}");
        }

        public void RoleShouldBeRemovedFromUser(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BusinessException($"User role couldn't removed\n {PrepareErrorMessageFromIdentityResult(result)}");
        }

        public async Task UserCantBeDeleteWhenUserIsACustomerAndHasOrder(User user)
        {
            if (await _userManager.IsInRoleAsync(user!, "customer") && _unitOfWork.ReadRepository<Order>().Any(x => x.UserId == user.Id))
                throw new BusinessException("User has order(s) so user can't be delete.");
        }

        public void UserShouldBeDeleted(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new BusinessException($"User role couldn't deleted\n {PrepareErrorMessageFromIdentityResult(result)}");
        }

        public void UserEmailShouldntBeTaken(string email, User? user = null)
        {
            if (user?.NormalizedEmail == email.ToUpperInvariant())
                return;

            if (_userManager.Users.Any(x => x.NormalizedEmail == email.ToUpperInvariant()))
                throw new BusinessException("The email is already taken");
        }

        public void UserNameShouldntBeTaken(string userName, User? user = null)
        {
            if (user?.NormalizedUserName == userName.ToUpperInvariant())
                return;

            if (_userManager.Users.Any(x => x.NormalizedUserName == userName.ToUpperInvariant()))
                throw new BusinessException("The username is already taken");
        }

        private string PrepareErrorMessageFromIdentityResult(IdentityResult result)
        {
            string errorMessage = "";
            foreach (var error in result.Errors)
                errorMessage += $"[{error.Code}] {error.Description}";
            return errorMessage;
        }
    }
}
