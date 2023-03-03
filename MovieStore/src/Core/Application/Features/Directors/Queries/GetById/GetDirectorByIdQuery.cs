using Application.Abstractions.Services;
using Application.Features.Directors.Dtos;
using MediatR;

namespace Application.Features.Directors.Queries.GetById
{
    public class GetDirectorByIdQuery : IRequest<DirectorGetByIdDto>
    {
        public string Id { get; set; } = null!;

        public class DirectorGetByIdQueryHandler : IRequestHandler<GetDirectorByIdQuery, DirectorGetByIdDto>
        {
            private readonly IDirectorService _directorService;

            public DirectorGetByIdQueryHandler(IDirectorService directorService)
            {
                _directorService = directorService;
            }

            public async Task<DirectorGetByIdDto> Handle(GetDirectorByIdQuery request, CancellationToken cancellationToken)
                => await _directorService.GetByIdAsync(request.Id);
        }
    }
}