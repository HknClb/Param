using Application.Abstractions.Services;
using Application.Features.Stars.Dtos;
using MediatR;

namespace Application.Features.Stars.Queries.GetById
{
    public class GetStarByIdQuery : IRequest<StarGetByIdDto>
    {
        public string Id { get; set; } = null!;

        public class StarGetByIdQueryHandler : IRequestHandler<GetStarByIdQuery, StarGetByIdDto>
        {
            private readonly IStarService _starService;

            public StarGetByIdQueryHandler(IStarService starService)
            {
                _starService = starService;
            }

            public async Task<StarGetByIdDto> Handle(GetStarByIdQuery request, CancellationToken cancellationToken)
                => await _starService.GetByIdAsync(request.Id);
        }
    }
}