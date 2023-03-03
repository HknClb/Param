using Application.Abstractions.Services;
using Application.DynamicQuery;
using Application.Features.Auth.Dtos;
using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using Application.Features.Users.Rules;
using AutoMapper;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Paging;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, UserBusinessRules userBusinessRules, IMapper mapper)
        {
            _userManager = userManager;
            _userBusinessRules = userBusinessRules;
            _mapper = mapper;
        }

        public async Task<UserGetByIdDto?> GetDetailsAsync(string id)
         => _mapper.Map<UserGetByIdDto?>(await _userManager.FindByIdAsync(id));

        public async Task<UsersListModel> GetListAsync(Dynamic dynamic, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<User> query = _userManager.Users.Include(x => x.Roles).ToDynamic(dynamic);
            if (!enableTracking)
                query.AsNoTracking();

            return _mapper.Map<UsersListModel>(await query.ToPaginateAsync(index, size, 0, cancellationToken));
        }

        public async Task<UserCreatedDto> CreateAsync(CreateUserDto createUser)
        {
            _userBusinessRules.UserEmailShouldntBeTaken(createUser.Email);
            _userBusinessRules.UserNameShouldntBeTaken(createUser.UserName);
            User user = new(createUser.UserName, createUser.Email, createUser.Name, createUser.Surname);
            IdentityResult result = await _userManager.CreateAsync(user, createUser.Password);
            _userBusinessRules.UserShouldBeCreated(result);

            if (createUser.Roles is not null)
                _userBusinessRules.RoleAssignmentToUserShouldBeSuccessful(await _userManager.AddToRolesAsync(user, createUser.Roles));

            return _mapper.Map<UserCreatedDto>(user);
        }

        public async Task<UserUpdatedDto> UpdateAsync(UpdateUserDto updateUser)
        {
            User? user = await _userManager.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == updateUser.Id);
            _userBusinessRules.UserShouldBeExist(user);

            if (updateUser.Email is not null)
                _userBusinessRules.UserEmailShouldntBeTaken(updateUser.Email, user);
            if (updateUser.UserName is not null)
                _userBusinessRules.UserNameShouldntBeTaken(updateUser.UserName, user);

            user = _mapper.Map(updateUser, user!);
            _userBusinessRules.UserShouldBeUpdated(await _userManager.UpdateAsync(user!));

            if (updateUser.Roles?.Any() == true)
            {
                // Deleting roles
                foreach (var role in await _userManager.GetRolesAsync(user))
                    if (!updateUser.Roles.Contains(role))
                        _userBusinessRules.RoleShouldBeRemovedFromUser(await _userManager.RemoveFromRoleAsync(user, role));

                // Adding roles
                foreach (var role in updateUser.Roles)
                    if (!await _userManager.IsInRoleAsync(user, role))
                        _userBusinessRules.RoleAssignmentToUserShouldBeSuccessful(await _userManager.AddToRoleAsync(user, role));
            }
            if (updateUser.Password is not null)
                _userBusinessRules.UserPasswordShouldBeChangedWhenUpdating(
                    await _userManager.ResetPasswordAsync(user!, await _userManager.GeneratePasswordResetTokenAsync(user!), updateUser.Password));

            return _mapper.Map<UserUpdatedDto>(user);
        }

        public async Task DeleteAsync(string id)
        {
            User? user = await _userManager.FindByIdAsync(id);
            _userBusinessRules.UserShouldBeExist(user);
            await _userBusinessRules.UserCantBeDeleteWhenUserIsACustomerAndHasOrder(user!);
            IdentityResult result = await _userManager.DeleteAsync(user!);
            _userBusinessRules.UserShouldBeDeleted(result);
        }

        public async Task UpdateRefreshTokenAsync(User user, TokenDto token, TimeSpan refreshTokenExpireTime)
        {
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiration = token.Expiration.Add(refreshTokenExpireTime);
            await _userManager.UpdateAsync(user);
        }
    }
}