using Application.Abstractions.Paging;
using Application.Abstractions.Services;
using Application.Abstractions.UnitOfWorks.Base;
using Application.DynamicQuery;
using Application.Features.Directors.Dtos;
using Application.Features.Directors.Models;
using Application.Features.Directors.Rules;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DirectorBusinessRules _directorBusinessRules;
        private readonly IMapper _mapper;

        public DirectorService(IUnitOfWork unitOfWork, DirectorBusinessRules directorBusinessRules, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _directorBusinessRules = directorBusinessRules;
            _mapper = mapper;
        }

        public async Task<DirectorGetByIdDto> GetByIdAsync(string id)
        {
            Director? director = await _unitOfWork.ReadRepository<Director>().GetAsync(x => x.Id == Guid.Parse(id), x => x.Include(x => x.Movies), false);
            _directorBusinessRules.DirectorShouldBeExist(director);
            return _mapper.Map<DirectorGetByIdDto>(director);
        }

        public async Task<DirectorsListModel> ListAsync(Dynamic dynamic, PageRequest pageRequest, CancellationToken cancellationToken = default)
        {
            IPaginate<Director> directors = await _unitOfWork.ReadRepository<Director>()
                .GetListByDynamicAsPaginateAsync(dynamic, x => x.IsActive, x => x.Include(x => x.Movies), pageRequest.Page, pageRequest.PageSize, false, cancellationToken);
            return _mapper.Map<DirectorsListModel>(directors);
        }

        public async Task<DirectorCreatedDto> CreateDirectorAsync(CreateDirectorDto createDirector)
        {
            Director director = await _unitOfWork.WriteRepository<Director>().AddAsync(_mapper.Map<Director>(createDirector));
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<DirectorCreatedDto>(director);
        }

        public async Task<DirectorUpdatedDto> UpdateDirectorAsync(UpdateDirectorDto updateDirector)
        {
            Director? director = await _unitOfWork.ReadRepository<Director>().GetAsync(x => x.Id == Guid.Parse(updateDirector.Id));
            _directorBusinessRules.DirectorShouldBeExist(director);
            director = _mapper.Map(updateDirector, director);
            director = await _unitOfWork.WriteRepository<Director>().UpdateAsync(director!);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<DirectorUpdatedDto>(director);
        }

        public async Task<DirectorDeletedDto> DeleteDirectorAsync(string id)
        {
            Director? director = await _unitOfWork.ReadRepository<Director>().GetAsync(x => x.Id == Guid.Parse(id));
            _directorBusinessRules.DirectorShouldBeExist(director);
            await _directorBusinessRules.IfTheDircetorHasDirectedAMovieItCantBeDeleted(director!);

            await _unitOfWork.WriteRepository<Director>().DeleteAsync(director!);
            await _unitOfWork.CompleteAsync();

            return new();
        }
    }
}