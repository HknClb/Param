using Application.Abstractions.Services;
using Application.Features.Movies.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Movies.Commands.Create
{
    public class CreateMovieCommand : IRequest<MovieCreatedDto>
    {
        public string Name { get; set; } = null!;
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public string DirectorId { get; set; } = null!;
        public string[] StarIds { get; set; } = null!;
        public int[] GenreIds { get; set; } = null!;

        public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieCreatedDto>
        {
            private readonly IMovieService _movieService;
            private readonly IMapper _mapper;

            public CreateMovieCommandHandler(IMovieService movieService, IMapper mapper)
            {
                _movieService = movieService;
                _mapper = mapper;
            }

            public async Task<MovieCreatedDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
                => await _movieService.CreateMovieAsync(_mapper.Map<CreateMovieDto>(request));
        }
    }
}