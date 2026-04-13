
using BookVerse.Application.DTOs.Auth;

namespace BookVerse.Application.InterfaceServices
{
public interface IAuthService
{
    Task RegisterAsync(RegisterRequest request);
}
}