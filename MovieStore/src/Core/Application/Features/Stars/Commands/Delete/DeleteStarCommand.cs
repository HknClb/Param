using Application.Abstractions.Services;
using Application.Features.Stars.Dtos;
using MediatR;

namespace Application.Features.Stars.Commands.Delete
{
    public class DeleteStarCommand : IRequest<StarDeletedDto>
    {
        public string Id { get; set; } = null!;

        public class DeleteStarCommandHandler : IRequestHandler<DeleteStarCommand, StarDeletedDto>
        {
            private readonly IStarService _starService;

            public DeleteStarCommandHandler(IStarService starService)
            {
                _starService = starService;
            }

            public async Task<StarDeletedDto> Handle(DeleteStarCommand request, CancellationToken cancellationToken)
                => await _starService.DeleteStarAsync(request.Id);
        }
    }
}
