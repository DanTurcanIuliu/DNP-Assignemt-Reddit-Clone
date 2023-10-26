using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreateDto dto);
    
    Task<ICollection<Post>> GetAsync(
        string? userName, 
        int? userId, 
        string? bodyContains, 
        string? titleContains
    );
}
