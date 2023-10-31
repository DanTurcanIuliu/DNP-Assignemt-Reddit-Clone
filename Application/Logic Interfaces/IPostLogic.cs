using Shared.DTOs;
using Shared.Models;

namespace Application.Logic_Interfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreateDto postToCreate);
}