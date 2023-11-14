using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class CommentHttpClient : ICommentService
{
    private readonly HttpClient client;

    public CommentHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(CommentCreateDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/comment", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Comment>> GetAsync(string? userName, int? userId, string? titleContains, string? bodyContains, int? postId)
    {
        string query = ConstructQuery(userName, userId, titleContains, bodyContains, postId);
        HttpResponseMessage response = await client.GetAsync("/Comment"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Comment> comments = JsonSerializer.Deserialize<ICollection<Comment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comments;
    }
    
    private static string ConstructQuery(string? userName, int? userId, string? titleContains, string? bodyContains, int? postId)
    {
        string query = "";
        if (!string.IsNullOrEmpty(userName))
        {
            query += $"?username={userName}";
        }

        if (userId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"userid={userId}";
        }

        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titleContains={titleContains}";
        }

        if (!string.IsNullOrEmpty(bodyContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"bodyContains={bodyContains}";
        }
        
        if (postId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"postId={postId}";
        }

        return query;
    }
}