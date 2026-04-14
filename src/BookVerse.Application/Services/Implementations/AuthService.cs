using BookVerse.Application.DTOs.Auth;
using BookVerse.Application.InterfaceServices;
using BookVerse.Domain.Entities;
using BookVerse.Domain.Interfaces;
using BookVerse.Application.Common;
using BookVerse.Application.DTOs.Users;

namespace BookVerse.Application.Services.Implementations;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // Check if the email is already registered
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            throw new Exception("Email already in use.");
        }

        // Check if the username is already taken
        if (await _userRepository.ExistsByUsernameAsync(request.UserName))
        {
            throw new Exception("Username already taken.");
        }

        //hash the password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);


        // Create a new user entity
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = passwordHash,
            Username = request.UserName,
            CreatedAt = DateTime.UtcNow,
            Role = "User"
        };

        // Save the user to the database
        await _userRepository.AddAsync(user);
    }

    
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        // 1. tìm user
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            throw new Exception("User not found");

        // 2. check password
        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isValid)
            throw new Exception("Wrong password");

        // 3. generate tokens
        var accessToken = JwtHelper.GenerateJwtToken(user);
        var refreshToken = JwtHelper.GenerateRefreshToken();

        // 4. map user response
        var userResponse = new UserResponse
        {
            Email = user.Email,
            UserName = user.Username,
            Role = user.Role
        };

        var result = new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = userResponse
        };

        return result;
    }

    public async Task Logout()
    {
        // Implement logout logic
        await Task.CompletedTask;
    }

}