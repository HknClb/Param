namespace Application.Features.Auth.Dtos
{
    public class SignedInDto
    {
        public SignedInDto()
        {
        }

        public SignedInDto(TokenDto token) : this()
        {
            Token = token;
        }

        public TokenDto Token { get; set; } = null!;
    }
}
