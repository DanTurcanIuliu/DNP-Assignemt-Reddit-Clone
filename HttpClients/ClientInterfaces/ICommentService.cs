using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task CreateAsync(CommentCreateDto dto);
    
    Task<ICollection<Comment>> GetAsync(
        string? userName, 
        int? userId, 
        string? titleContains, 
        string? bodyContains,
        int? postId
    );
}