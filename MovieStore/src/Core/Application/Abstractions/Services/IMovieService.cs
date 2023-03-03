using Application.DynamicQuery;
using Application.Features.Movies.Dtos;
using Application.Features.Movies.Models;
using Application.Requests;

namespace Application.Abstractions.Services
{
    public interface IMovieService
    {
        public Task<MovieGetByIdDto> GetByIdAsync(string id);
        public Task<MoviesListModel> ListAsync(Dynamic dynamic, PageRequest pageRequest, CancellationToken cancellationToken = default);
        public Task<MovieCreatedDto> CreateMovieAsync(CreateMovieDto createMovie);
        public Task<MovieUpdatedDto> UpdateMovieAsync(UpdateMovieDto updateMovie);
        public Task<MovieDeletedDto> DeleteMovieAsync(string id);
    }
}
