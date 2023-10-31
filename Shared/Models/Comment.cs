namespace Shared.Models;

public class Comment
{
    public int Id { get; set; }
    public User Author { get; }
    public string Body { get; }


    public Comment(User author, string body)
    {
        Author = author;
        Body = body;
    }
}