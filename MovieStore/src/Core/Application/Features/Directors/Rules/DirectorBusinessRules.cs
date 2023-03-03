using Application.Abstractions.UnitOfWorks.Base;
using CrossCuttingConcerns.Exceptions.Business;
using Domain.Entities;

namespace Application.Features.Directors.Rules
{
    public class DirectorBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public DirectorBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DirectorShouldBeExist(Director? director)
        {
            if (director is null)
                throw new BusinessException("Director is not exist");
        }

        public async Task IfTheDircetorHasDirectedAMovieItCantBeDeleted(Director director)
        {
            if (await _unitOfWork.ReadRepository<Movie>().AnyAsync(x => x.DirectorId == director.Id))
                throw new BusinessException("The director can't be deleted because directed a movie.");
        }
    }
}
