using Application.Abstractions.UnitOfWorks.Base;
using CrossCuttingConcerns.Exceptions.Business;
using Domain.Entities;

namespace Application.Features.Movies.Rules
{
    public class MovieBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void GenreShouldBeExist(Genre? genre)
        {
            if (genre is null)
                throw new BusinessException("Genre is not exist");
        }

        public void StarShouldBeExist(Star? star)
        {
            if (star is null)
                throw new BusinessException("Star is not exist");
        }

        public void MovieShouldBeExist(Movie? movie)
        {
            if (movie is null)
                throw new BusinessException("Movie is not exist");
        }

        public async Task IfTheMovieOrderedItCantBeDeleted(Guid id)
        {
            if (await _unitOfWork.ReadRepository<Order>().AnyAsync(x => x.MovieId == id))
                throw new BusinessException("The movie can't delete because ordered or added to basket.");
        }
    }
}
