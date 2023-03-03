using Application.Abstractions.Services;
using Application.DynamicQuery;
using Application.Features.Directors.Models;
using Application.Requests;
using MediatR;

namespace Application.Features.Directors.Queries.List
{
    public class GetDirectorsListQuery : IRequest<DirectorsListModel>
    {
        public Dynamic? Dynamic { get; set; }
        public PageRequest? PageRequest { get; set; }

        public class GetDirectorsListQueryHandler : IRequestHandler<GetDirectorsListQuery, DirectorsListModel>
        {
            private readonly IDirectorService _directorService;

            public GetDirectorsListQueryHandler(IDirectorService directorService)
            {
                _directorService = directorService;
            }

            public async Task<DirectorsListModel> Handle(GetDirectorsListQuery request, CancellationToken cancellationToken)
                => await _directorService.ListAsync(request.Dynamic ?? new(), request.PageRequest ?? new(), cancellationToken);
        }
    }
}
