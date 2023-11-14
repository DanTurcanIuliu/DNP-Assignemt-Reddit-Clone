using Shared.Models;

namespace Application.Dao_Interfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    
    Task<User?> GetByIdAsync(int id);

    Task<User> ValidateUser(string username, string password);
}