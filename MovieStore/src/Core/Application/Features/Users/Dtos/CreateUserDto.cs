namespace Application.Features.Users.Dtos
{
    public class CreateUserDto
    {
        public CreateUserDto()
        {
        }

        public CreateUserDto(string name, string surname, string userName, string email, string password, string? phoneNumber, string[]? roles) : this()
        {
            Name = name;
            Surname = surname;
            UserName = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Roles = roles;
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string[]? Roles { get; set; }
    }
}
