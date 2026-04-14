using BookVerse.Application.DTOs.Users;

namespace BookVerse.Application.DTOs.Auth;
public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    public UserResponse User { get; set; }
}