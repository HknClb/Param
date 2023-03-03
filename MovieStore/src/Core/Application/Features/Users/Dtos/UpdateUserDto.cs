namespace Application.Features.Users.Dtos
{
    public class UpdateUserDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? Surname { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; } = null!;
        public string[]? Roles { get; set; }
        public bool IsActive { get; set; }
    }
}
