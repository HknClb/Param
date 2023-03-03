using Application.Abstractions.Services;
using Application.Features.Stars.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Stars.Commands.Update
{
    public class UpdateStarCommand : IRequest<StarUpdatedDto>
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public bool IsActive { get; set; } = true;

        public class UpdateStarCommandHandler : IRequestHandler<UpdateStarCommand, StarUpdatedDto>
        {
            private readonly IStarService _starService;
            private readonly IMapper _mapper;

            public UpdateStarCommandHandler(IStarService starService, IMapper mapper)
            {
                _starService = starService;
                _mapper = mapper;
            }

            public async Task<StarUpdatedDto> Handle(UpdateStarCommand request, CancellationToken cancellationToken)
                => await _starService.UpdateStarAsync(_mapper.Map<UpdateStarDto>(request));
        }
    }
}