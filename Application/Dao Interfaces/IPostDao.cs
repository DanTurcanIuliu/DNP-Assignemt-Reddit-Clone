using Shared.Models;

namespace Application.Dao_Interfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    
    Task<User?> GetByIdAsync(int id);
}