using Application.DynamicQuery;
using Application.Features.Stars.Dtos;
using Application.Features.Stars.Models;
using Application.Requests;

namespace Application.Abstractions.Services
{
    public interface IStarService
    {
        public Task<StarGetByIdDto> GetByIdAsync(string id);
        public Task<StarsListModel> ListAsync(Dynamic dynamic, PageRequest pageRequest, CancellationToken cancellationToken = default);
        public Task<StarCreatedDto> CreateStarAsync(CreateStarDto createStar);
        public Task<StarUpdatedDto> UpdateStarAsync(UpdateStarDto updateStar);
        public Task<StarDeletedDto> DeleteStarAsync(string id);
    }
}