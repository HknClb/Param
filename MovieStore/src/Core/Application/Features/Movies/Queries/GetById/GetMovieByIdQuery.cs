using Application.Abstractions.Services;
using Application.Features.Movies.Dtos;
using MediatR;

namespace Application.Features.Movies.Queries.GetById
{
    public class GetMovieByIdQuery : IRequest<MovieGetByIdDto>
    {
        public string Id { get; set; } = null!;

        public class MovieGetByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieGetByIdDto>
        {
            private readonly IMovieService _movieService;

            public MovieGetByIdQueryHandler(IMovieService movieService)
            {
                _movieService = movieService;
            }

            public async Task<MovieGetByIdDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
                => await _movieService.GetByIdAsync(request.Id);
        }
    }
}