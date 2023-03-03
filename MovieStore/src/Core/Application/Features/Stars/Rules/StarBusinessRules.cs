using Application.Abstractions.UnitOfWorks.Base;
using CrossCuttingConcerns.Exceptions.Business;
using Domain.Entities;

namespace Application.Features.Stars.Rules
{
    public class StarBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public StarBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void StarShouldBeExist(Star? star)
        {
            if (star is null)
                throw new BusinessException("Star is not exist");
        }

        public async Task IfTheStarHasActedAMovieItCantBeDeleted(Star star)
        {
            if (await _unitOfWork.ReadRepository<Movie>().AnyAsync(x => x.Stars.Contains(star)))
                throw new BusinessException("The star can't be deleted because acted in a movie.");
        }
    }
}
