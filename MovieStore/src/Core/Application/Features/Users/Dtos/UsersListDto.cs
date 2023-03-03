namespace Application.Features.Users.Dtos
{
    public class UsersListDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string[]? Roles { get; set; }
    }
}
