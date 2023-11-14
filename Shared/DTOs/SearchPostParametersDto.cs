namespace Shared.DTOs;

public class SearchPostParametersDto
{
    public string? Username { get;}
    public string? TitleContains { get;}
    public string? BodyContains { get;}
    public int? UserId { get;}

    public SearchPostParametersDto(string? username, string? titleContains, string? bodyContains, int? userId)
    {
        Username = username;
        TitleContains = titleContains;
        BodyContains = bodyContains;
        UserId = userId;
    }
}