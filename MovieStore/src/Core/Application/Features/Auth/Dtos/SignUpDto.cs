namespace Application.Features.Auth.Dtos
{
    public class SignUpDto
    {
        public SignUpDto()
        {
        }

        public SignUpDto(string name, string surname, string email, string userName, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            UserName = userName;
            Password = password;
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
