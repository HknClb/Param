using Application.Abstractions.Paging;
using Application.Abstractions.Services;
using Application.Abstractions.UnitOfWorks.Base;
using Application.DynamicQuery;
using Application.Features.Movies.Dtos;
using Application.Features.Movies.Models;
using Application.Features.Movies.Rules;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MovieBusinessRules _movieBusinessRules;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, MovieBusinessRules movieBusinessRules, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _movieBusinessRules = movieBusinessRules;
            _mapper = mapper;
        }

        public async Task<MovieGetByIdDto> GetByIdAsync(string id)
        {
            Movie? movie = await _unitOfWork.ReadRepository<Movie>()
                .GetAsync(x => x.Id == Guid.Parse(id), x => x.Include(x => x.Director).Include(x => x.Stars).Include(x => x.Genres), false);
            _movieBusinessRules.MovieShouldBeExist(movie);

            return _mapper.Map<MovieGetByIdDto>(movie);
        }

        public async Task<MoviesListModel> ListAsync(Dynamic dynamic, PageRequest pageRequest, CancellationToken cancellationToken = default)
        {
            IPaginate<Movie> movies = await _unitOfWork.ReadRepository<Movie>()
                .GetListByDynamicAsPaginateAsync(dynamic, x => x.IsActive, x => x.Include(x => x.Director).Include(x => x.Stars.Where(x => x.IsActive)).Include(x => x.Genres),
                pageRequest.Page, pageRequest.PageSize, false, cancellationToken);

            return _mapper.Map<MoviesListModel>(movies);
        }

        public async Task<MovieCreatedDto> CreateMovieAsync(CreateMovieDto createMovie)
        {
            Movie movie = _mapper.Map<Movie>(createMovie);

            foreach (var genreId in createMovie.GenreIds)
            {
                Genre? genre = await _unitOfWork.ReadRepository<Genre>().GetAsync(x => x.Id == genreId);
                _movieBusinessRules.GenreShouldBeExist(genre);
                movie.Genres.Add(genre!);
            }
            foreach (var starId in createMovie.StarIds)
            {
                Star? star = await _unitOfWork.ReadRepository<Star>().GetAsync(x => x.Id == Guid.Parse(starId));
                _movieBusinessRules.StarShouldBeExist(star);
                movie.Stars.Add(star!);
            }

            movie = await _unitOfWork.WriteRepository<Movie>().AddAsync(movie);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<MovieCreatedDto>(movie);
        }

        public async Task<MovieUpdatedDto> UpdateMovieAsync(UpdateMovieDto updateMovie)
        {
            Movie? movie = await _unitOfWork.ReadRepository<Movie>()
               .GetAsync(x => x.Id == Guid.Parse(updateMovie.Id), x => x.Include(x => x.Stars).Include(x => x.Genres));
            _movieBusinessRules.MovieShouldBeExist(movie);
            movie = _mapper.Map(updateMovie, movie);

            if (updateMovie.GenreIds is not null)
            {
                movie!.Genres.Clear();
                foreach (var genreId in updateMovie.GenreIds)
                {
                    Genre? genre = await _unitOfWork.ReadRepository<Genre>().GetAsync(x => x.Id == genreId);
                    _movieBusinessRules.GenreShouldBeExist(genre);
                    movie.Genres.Add(genre!);
                }
            }
            if (updateMovie.StarIds is not null)
            {
                movie!.Stars = movie!.Stars.Where(x => !x.IsActive).ToList();
                foreach (var starId in updateMovie.StarIds)
                {
                    Star? star = await _unitOfWork.ReadRepository<Star>().GetAsync(x => x.Id == Guid.Parse(starId) && x.IsActive);
                    _movieBusinessRules.StarShouldBeExist(star);
                    movie.Stars.Add(star!);
                }
            }

            movie = await _unitOfWork.WriteRepository<Movie>().UpdateAsync(movie!);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<MovieUpdatedDto>(movie);
        }

        public async Task<MovieDeletedDto> DeleteMovieAsync(string id)
        {
            Movie? movie = await _unitOfWork.ReadRepository<Movie>().GetAsync(x => x.Id == Guid.Parse(id));
            _movieBusinessRules.MovieShouldBeExist(movie);
            await _movieBusinessRules.IfTheMovieOrderedItCantBeDeleted(movie!.Id);

            await _unitOfWork.WriteRepository<Movie>().DeleteAsync(movie);
            await _unitOfWork.CompleteAsync();

            return new();
        }
    }
}