using Shared.Models;

namespace Application.Dao_Interfaces;

public interface ICommentDao
{
    Task<Comment> CreateAsync(Comment comment);
}