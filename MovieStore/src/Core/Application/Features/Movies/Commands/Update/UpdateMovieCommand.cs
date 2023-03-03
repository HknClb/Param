using Application.Abstractions.Services;
using Application.Features.Movies.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Movies.Commands.Update
{
    public class UpdateMovieCommand : IRequest<MovieUpdatedDto>
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public int? PublishedYear { get; set; }
        public decimal? Price { get; set; }
        public string? DirectorId { get; set; }
        public string[]? StarIds { get; set; }
        public int[]? GenreIds { get; set; }

        public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, MovieUpdatedDto>
        {
            private readonly IMovieService _movieService;
            private readonly IMapper _mapper;

            public UpdateMovieCommandHandler(IMovieService movieService, IMapper mapper)
            {
                _movieService = movieService;
                _mapper = mapper;
            }

            public async Task<MovieUpdatedDto> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
                => await _movieService.UpdateMovieAsync(_mapper.Map<UpdateMovieDto>(request));
        }
    }
}
