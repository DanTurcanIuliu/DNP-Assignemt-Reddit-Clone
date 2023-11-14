namespace Shared.Models;

public class Comment
{
    public int Id { get; set; }
    public User Author { get; }
    
    public Post Reference { get; }
    public string Body { get; }


    public Comment(User author, Post reference, string body)
    {
        Author = author;
        Reference = reference;
        Body = body;
    }
}