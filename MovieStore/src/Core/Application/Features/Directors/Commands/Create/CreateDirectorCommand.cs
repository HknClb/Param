using Application.Abstractions.Services;
using Application.Features.Directors.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Directors.Commands.Create
{
    public class CreateDirectorCommand : IRequest<DirectorCreatedDto>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, DirectorCreatedDto>
        {
            private readonly IDirectorService _directorService;
            private readonly IMapper _mapper;

            public CreateDirectorCommandHandler(IDirectorService directorService, IMapper mapper)
            {
                _directorService = directorService;
                _mapper = mapper;
            }

            public async Task<DirectorCreatedDto> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
                => await _directorService.CreateDirectorAsync(_mapper.Map<CreateDirectorDto>(request));
        }
    }
}
