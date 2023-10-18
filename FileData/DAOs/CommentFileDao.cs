using Application.Dao_Interfaces;
using Application.Provider_Interfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class CommentFileDao:ICommentDao, ICommentProvider
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

    public Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParams)
    {
        IEnumerable<Comment> result = context.Comments.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            result = context.Comments.Where(todo =>
                todo.Author.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParams.UserId != null)
        {
            result = result.Where(t => t.Author.Id == searchParams.UserId);
        }

        
        if (!string.IsNullOrEmpty(searchParams.BodyContains))
        {
            result = result.Where(t =>
                t.Body.Contains(searchParams.BodyContains, StringComparison.OrdinalIgnoreCase));
        }
        
        if (!string.IsNullOrEmpty(searchParams.PostTitleContains))
        {
            result = result.Where(t =>
                t.Reference.Title.Contains(searchParams.PostTitleContains, StringComparison.OrdinalIgnoreCase));
        }
        
        if (searchParams.PostId != null)
        {
            result = result.Where(t => t.Reference.Id == searchParams.PostId);
        }

        return Task.FromResult(result);
    }
}