using Application.Abstractions.Services;
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using MediatR;

namespace Application.Features.Users.Queries.GetById
{
    public class GetUserByIdQuery : IRequest<UserGetByIdDto>
    {
        public string Id { get; set; } = null!;

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserGetByIdDto>
        {
            private readonly IUserService _userService;
            private readonly UserBusinessRules _userBusinessRules;

            public GetUserByIdQueryHandler(IUserService userService, UserBusinessRules userBusinessRules)
            {
                _userService = userService;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UserGetByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                UserGetByIdDto? userGetByIdDto = await _userService.GetDetailsAsync(request.Id);
                _userBusinessRules.UserShouldBeExistWhenGetById(userGetByIdDto);
                return userGetByIdDto!;
            }
        }
    }
}
