using Application.Abstractions.Services;
using Application.Features.Directors.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Directors.Commands.Update
{
    public class UpdateDirectorCommand : IRequest<DirectorUpdatedDto>
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public bool IsActive { get; set; } = true;

        public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand, DirectorUpdatedDto>
        {
            private readonly IDirectorService _directorService;
            private readonly IMapper _mapper;

            public UpdateDirectorCommandHandler(IDirectorService directorService, IMapper mapper)
            {
                _directorService = directorService;
                _mapper = mapper;
            }

            public async Task<DirectorUpdatedDto> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
                => await _directorService.UpdateDirectorAsync(_mapper.Map<UpdateDirectorDto>(request));
        }
    }
}