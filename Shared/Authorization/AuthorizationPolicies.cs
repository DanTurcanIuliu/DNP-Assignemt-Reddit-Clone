using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Authorization;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            
        });
    }
}