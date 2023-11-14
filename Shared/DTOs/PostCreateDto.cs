using Shared.Models;

namespace Shared.DTOs;

public class PostCreateDto
{
    public int AuthorId { get; }
    public string Title { get; }
    public string Body { get; }
    
    public PostCreateDto(int authorId, string title, string body)
    {
        AuthorId = authorId;
        Title = title;
        Body = body;
    }  
}