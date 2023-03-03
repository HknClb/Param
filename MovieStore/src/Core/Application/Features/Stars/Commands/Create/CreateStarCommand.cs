using Application.Abstractions.Services;
using Application.Features.Stars.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Stars.Commands.Create
{
    public class CreateStarCommand : IRequest<StarCreatedDto>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public class CreateStarCommandHandler : IRequestHandler<CreateStarCommand, StarCreatedDto>
        {
            private readonly IStarService _starService;
            private readonly IMapper _mapper;

            public CreateStarCommandHandler(IStarService starService, IMapper mapper)
            {
                _starService = starService;
                _mapper = mapper;
            }

            public async Task<StarCreatedDto> Handle(CreateStarCommand request, CancellationToken cancellationToken)
                => await _starService.CreateStarAsync(_mapper.Map<CreateStarDto>(request));
        }
    }
}
