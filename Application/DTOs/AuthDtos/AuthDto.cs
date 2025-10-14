namespace Services.DTOs.AuthDtos
{
    public class AuthDto
    {
        public record RegisterDto(string UserName,string Email,  string Password);
        public record LoginDto(string Email, string Password);
        public record AuthResponseDto(string AccessToken);
    }
}
