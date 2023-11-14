using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class Comment
{

    public int Id { get; set; }
    public User Author { get; set;}
    public int AuthorId { get; set; }
    public Post Reference { get; set;}
    public int ReferenceId { get; set; }
    public string Body { get; set;}


    public Comment(User author, Post reference, string body)
    {
        Author = author;
        Reference = reference;
        Body = body;
    }
    
    private Comment(){}
}