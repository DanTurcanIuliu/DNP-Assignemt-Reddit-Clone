namespace Shared.DTOs;

public class SearchCommentParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public string? PostTitleContains { get;}
    public int? PostId { get;}
    public string? BodyContains { get;}
    

    public SearchCommentParametersDto(string? username, string? titleContains, string? bodyContains, int? userId, int? postId)
    {
        Username = username;
        PostTitleContains = titleContains;
        BodyContains = bodyContains;
        UserId = userId;
        PostId = postId;
    }
}