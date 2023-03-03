using Application.Abstractions.Services;
using Application.DynamicQuery;
using Application.Features.Movies.Models;
using Application.Requests;
using MediatR;

namespace Application.Features.Movies.Queries.List
{
    public class GetMoviesListQuery : IRequest<MoviesListModel>
    {
        public Dynamic? Dynamic { get; set; }
        public PageRequest? PageRequest { get; set; }

        public class GetMoviesListQueryHandler : IRequestHandler<GetMoviesListQuery, MoviesListModel>
        {
            private readonly IMovieService _movieService;

            public GetMoviesListQueryHandler(IMovieService movieService)
            {
                _movieService = movieService;
            }

            public async Task<MoviesListModel> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
                => await _movieService.ListAsync(request.Dynamic ?? new(), request.PageRequest ?? new(), cancellationToken);
        }
    }
}
