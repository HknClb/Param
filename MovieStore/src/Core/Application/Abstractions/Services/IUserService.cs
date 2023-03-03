using Application.DynamicQuery;
using Application.Features.Auth.Dtos;
using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using Domain.Entities.Identity;

namespace Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserGetByIdDto?> GetDetailsAsync(string id);
        Task<UsersListModel> GetListAsync(Dynamic dynamic, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default);
        Task<UserCreatedDto> CreateAsync(CreateUserDto createUser);
        Task<UserUpdatedDto> UpdateAsync(UpdateUserDto updateUser);
        Task DeleteAsync(string id);
        Task UpdateRefreshTokenAsync(User user, TokenDto token, TimeSpan refreshTokenExpireTime);
    }
}
