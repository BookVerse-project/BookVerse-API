
using BookVerse.Application.DTOs.Auth;

namespace BookVerse.Application.InterfaceServices
{
public interface IAuthService
{
    Task RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task Logout();
}
}