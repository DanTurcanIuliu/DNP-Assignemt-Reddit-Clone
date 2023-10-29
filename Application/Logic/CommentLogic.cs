using Application.Dao_Interfaces;
using Application.Logic_Interfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class CommentLogic:ICommentLogic
{
    private readonly ICommentDao commentDao;
    private readonly IUserDao userDao;
    private readonly IPostDao postDao;

    public CommentLogic(ICommentDao commentDao, IUserDao userDao, IPostDao postDao)
    {
        this.commentDao = commentDao;
        this.userDao = userDao;
        this.postDao = postDao;
    }
    
    public async Task<Comment> CreateAsync(CommentCreateDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.AuthorId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.AuthorId} was not found.");
        }
        
        Post? post = await postDao.GetByIdAsync(dto.PostId);
        if (user == null)
        {
            throw new Exception($"Post with id {dto.PostId} was not found.");
        }

        ValidateComment(dto);
        Comment comment = new Comment(user, post, dto.Body);
        Comment created = await commentDao.CreateAsync(comment);
        return created;
    }

    private void ValidateComment(CommentCreateDto dto)
    {
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Comment cannot be empty.");
    }
}