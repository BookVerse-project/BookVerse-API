using BookVerse.Domain.Entities;


namespace BookVerse.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
}
