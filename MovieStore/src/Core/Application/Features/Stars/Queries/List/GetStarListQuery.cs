using Application.Abstractions.Services;
using Application.DynamicQuery;
using Application.Features.Stars.Models;
using Application.Requests;
using MediatR;

namespace Application.Features.Stars.Queries.List
{
    public class GetStarsListQuery : IRequest<StarsListModel>
    {
        public Dynamic? Dynamic { get; set; }
        public PageRequest? PageRequest { get; set; }

        public class GetStarsListQueryHandler : IRequestHandler<GetStarsListQuery, StarsListModel>
        {
            private readonly IStarService _starService;

            public GetStarsListQueryHandler(IStarService starService)
            {
                _starService = starService;
            }

            public async Task<StarsListModel> Handle(GetStarsListQuery request, CancellationToken cancellationToken)
                => await _starService.ListAsync(request.Dynamic ?? new(), request.PageRequest ?? new(), cancellationToken);
        }
    }
}
