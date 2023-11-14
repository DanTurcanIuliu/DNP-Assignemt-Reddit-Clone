using Shared.DTOs;
using Shared.Models;

namespace Application.Logic_Interfaces;

public interface ICommentLogic
{
    Task<Comment> CreateAsync(CommentCreateDto commentToCreate);
}