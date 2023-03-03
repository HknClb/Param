using Application.Abstractions.Paging;
using Application.Features.Users.Dtos;

namespace Application.Features.Users.Models
{
    public class UsersListModel : BasePageableModel
    {
        public IList<UsersListDto> Items { get; set; } = null!;
    }
}
