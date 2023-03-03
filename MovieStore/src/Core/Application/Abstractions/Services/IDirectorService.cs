using Application.DynamicQuery;
using Application.Features.Directors.Dtos;
using Application.Features.Directors.Models;
using Application.Requests;

namespace Application.Abstractions.Services
{
    public interface IDirectorService
    {
        public Task<DirectorGetByIdDto> GetByIdAsync(string id);
        public Task<DirectorsListModel> ListAsync(Dynamic dynamic, PageRequest pageRequest, CancellationToken cancellationToken = default);
        public Task<DirectorCreatedDto> CreateDirectorAsync(CreateDirectorDto createDirector);
        public Task<DirectorUpdatedDto> UpdateDirectorAsync(UpdateDirectorDto updateDirector);
        public Task<DirectorDeletedDto> DeleteDirectorAsync(string id);
    }
}