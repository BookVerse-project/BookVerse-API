using BookVerse.Application.DTOs.Auth;
using BookVerse.Application.InterfaceServices;
using BookVerse.Domain.Entities;
using BookVerse.Domain.Interfaces;
using BCrypt.Net;

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

}