using Application.Dao_Interfaces;
using Application.Provider_Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class PostEfcDao  : IPostDao, IPostProvider
{
    private readonly MyPostContext context;
    
    private UserEfcDao userEfcDao;

    public PostEfcDao(MyPostContext context)
    {
        this.context = context;
        this.userEfcDao = new UserEfcDao(context);
    }
    
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Post?> GetByIdAsync(int dtoAuthorId)
    {
        Post? post = await context.Posts.FindAsync(dtoAuthorId);
        return post;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Author).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            query = query.Where(post =>
                post.Author.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }
    
        if (searchParameters.UserId != null)
        {
            query = query.Where(post => post.Author.Id == searchParameters.UserId);
        }
    
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(searchParameters.BodyContains))
        {
            query = query.Where(t =>
                t.Body.ToLower().Contains(searchParameters.BodyContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }
}