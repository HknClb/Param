using Application.Abstractions.Services;
using Application.Features.Directors.Dtos;
using MediatR;

namespace Application.Features.Directors.Commands.Delete
{
    public class DeleteDirectorCommand : IRequest<DirectorDeletedDto>
    {
        public string Id { get; set; } = null!;

        public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, DirectorDeletedDto>
        {
            private readonly IDirectorService _directorService;

            public DeleteDirectorCommandHandler(IDirectorService directorService)
            {
                _directorService = directorService;
            }

            public async Task<DirectorDeletedDto> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
                => await _directorService.DeleteDirectorAsync(request.Id);
        }
    }
}
