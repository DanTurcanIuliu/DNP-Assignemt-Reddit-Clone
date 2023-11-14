using Application.Dao_Interfaces;
using Application.Logic_Interfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class PostLogic: IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreateDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.AuthorId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.AuthorId} was not found.");
        }

        ValidatePost(dto);
        Post post = new Post(user, dto.Title, dto.Body);
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    private void ValidatePost(PostCreateDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
    }
}