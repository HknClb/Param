using Application.Abstractions.Services;
using Application.Features.Movies.Dtos;
using MediatR;

namespace Application.Features.Movies.Commands.Delete
{
    public class DeleteMovieCommand : IRequest<MovieDeletedDto>
    {
        public string Id { get; set; } = null!;

        public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, MovieDeletedDto>
        {
            private readonly IMovieService _movieService;

            public DeleteMovieCommandHandler(IMovieService movieService)
            {
                _movieService = movieService;
            }

            public async Task<MovieDeletedDto> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
                => await _movieService.DeleteMovieAsync(request.Id);
        }
    }
}
