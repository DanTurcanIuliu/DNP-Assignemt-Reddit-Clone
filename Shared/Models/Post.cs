using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    
    public User Author { get; set; }
    public int UserId { get; set; }
    
    //public ICollection<Comment> Comments { get; set; }

    

    public Post(User author, string title, string body)
    {
        Author = author;
        Title = title;
        Body = body;
    }
    
    private Post(){}
    
    
}