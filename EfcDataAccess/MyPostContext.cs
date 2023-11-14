using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess;

public class MyPostContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/MyPost.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);            
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
        modelBuilder.Entity<Comment>().HasKey(comment => comment.Id);
    }
}