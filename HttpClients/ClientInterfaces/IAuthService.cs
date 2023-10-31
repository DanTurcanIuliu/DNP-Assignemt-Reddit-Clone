using System.Security.Claims;

namespace HttpClients.ClientInterfaces;

public interface IAuthService
{
    Task LoginAsync(string username, string password);
    Task LogoutAsync();
    Task<ClaimsPrincipal> GetAuthAsync();
    Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}