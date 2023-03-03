using Application.Abstractions.Paging;
using Application.Abstractions.Services;
using Application.Abstractions.UnitOfWorks.Base;
using Application.DynamicQuery;
using Application.Features.Stars.Dtos;
using Application.Features.Stars.Models;
using Application.Features.Stars.Rules;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class StarService : IStarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StarBusinessRules _starBusinessRules;
        private readonly IMapper _mapper;

        public StarService(IUnitOfWork unitOfWork, StarBusinessRules starBusinessRules, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _starBusinessRules = starBusinessRules;
            _mapper = mapper;
        }

        public async Task<StarGetByIdDto> GetByIdAsync(string id)
        {
            Star? star = await _unitOfWork.ReadRepository<Star>().GetAsync(x => x.Id == Guid.Parse(id), x => x.Include(x => x.Movies), false);
            _starBusinessRules.StarShouldBeExist(star);
            return _mapper.Map<StarGetByIdDto>(star);
        }

        public async Task<StarsListModel> ListAsync(Dynamic dynamic, PageRequest pageRequest, CancellationToken cancellationToken = default)
        {
            IPaginate<Star> stars = await _unitOfWork.ReadRepository<Star>()
                .GetListByDynamicAsPaginateAsync(dynamic, x => x.IsActive, x => x.Include(x => x.Movies), pageRequest.Page, pageRequest.PageSize, false, cancellationToken);
            return _mapper.Map<StarsListModel>(stars);
        }

        public async Task<StarCreatedDto> CreateStarAsync(CreateStarDto createStar)
        {
            Star star = await _unitOfWork.WriteRepository<Star>().AddAsync(_mapper.Map<Star>(createStar));
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<StarCreatedDto>(star);
        }

        public async Task<StarUpdatedDto> UpdateStarAsync(UpdateStarDto updateStar)
        {
            Star? star = await _unitOfWork.ReadRepository<Star>().GetAsync(x => x.Id == Guid.Parse(updateStar.Id));
            _starBusinessRules.StarShouldBeExist(star);
            star = _mapper.Map(updateStar, star);
            star = await _unitOfWork.WriteRepository<Star>().UpdateAsync(star!);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<StarUpdatedDto>(star);
        }

        public async Task<StarDeletedDto> DeleteStarAsync(string id)
        {
            Star? star = await _unitOfWork.ReadRepository<Star>().GetAsync(x => x.Id == Guid.Parse(id));
            _starBusinessRules.StarShouldBeExist(star);
            await _starBusinessRules.IfTheStarHasActedAMovieItCantBeDeleted(star!);
            await _unitOfWork.WriteRepository<Star>().DeleteAsync(star!);
            await _unitOfWork.CompleteAsync();
            return new();
        }
    }
}