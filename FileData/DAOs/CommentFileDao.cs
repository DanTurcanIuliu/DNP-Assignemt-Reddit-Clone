using Application.Dao_Interfaces;
using Shared.Models;

namespace FileData.DAOs;

public class CommentFileDao:ICommentDao
{
    private readonly FileContext context;

    public CommentFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Comment> CreateAsync(Comment comment)
    {
        int id = 1;
        if (context.Comments.Any())
        {
            id = context.Comments.Max(t => t.Id);
            id++;
        }

        comment.Id = id;
        
        context.Comments.Add(comment);
        context.SaveChanges();

        return Task.FromResult(comment);
    }
}