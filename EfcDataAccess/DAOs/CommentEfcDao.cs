using Application.Dao_Interfaces;
using Application.Provider_Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao, ICommentProvider
{
    private readonly MyPostContext context;

    public CommentEfcDao(MyPostContext context)
    {
        this.context = context;
    }
    
    public async Task<Comment> CreateAsync(Comment comment)
    {
        EntityEntry<Comment> added = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
    {
        IQueryable<Comment> query = context.Comments.AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            query = query.Where(comment =>
                comment.Author.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }
    
        if (searchParameters.UserId != null)
        {
            query = query.Where(comment => comment.Author.Id == searchParameters.UserId);
        }
        
        if (searchParameters.PostId != null)
        {
            query = query.Where(comment => comment.Reference.Id == searchParameters.PostId);
        }
    
        if (!string.IsNullOrEmpty(searchParameters.BodyContains))
        {
            query = query.Where(comment =>
                comment.Body.ToLower().Contains(searchParameters.BodyContains.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(searchParameters.PostTitleContains))
        {
            query = query.Where(comment =>
                comment.Reference.Title.ToLower().Contains(searchParameters.PostTitleContains.ToLower()));
        }

        List<Comment> result = await query.ToListAsync();
        return result;
    }
}