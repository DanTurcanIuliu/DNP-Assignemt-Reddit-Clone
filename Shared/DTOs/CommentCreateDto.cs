using Shared.Models;

namespace Shared.DTOs;

public class CommentCreateDto
{
    public int AuthorId { get; }
    public int PostId { get; }
    public string Body { get; }


    public CommentCreateDto(int authorId, int postId, string body)
    {
        AuthorId = authorId;
        PostId = postId;
        Body = body;
    }
}