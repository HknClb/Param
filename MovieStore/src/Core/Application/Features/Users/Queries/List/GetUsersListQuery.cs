using Application.Abstractions.Services;
using Application.DynamicQuery;
using Application.Features.Users.Models;
using Application.Requests;
using MediatR;

namespace Application.Features.Users.Queries.List
{
    public class GetUsersListQuery : IRequest<UsersListModel>, IDynamicQuery
    {
        public Dynamic? Dynamic { get; set; }
        public PageRequest? Paginate { get; set; }

        public class GetUserListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListModel>
        {
            private readonly IUserService _userService;

            public GetUserListQueryHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<UsersListModel> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
                => await _userService.GetListAsync(request.Dynamic ?? new(), request.Paginate?.Page ?? 0, request.Paginate?.PageSize ?? 10, false, cancellationToken);
        }
    }
}
