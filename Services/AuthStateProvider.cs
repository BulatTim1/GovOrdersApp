using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class AuthStateProvider : AuthenticationStateProvider
{
    public static bool IsAuthenticated { get; set; }
    public static string UserName { get; set; }
    public static string Role { get; set; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity;

        if (IsAuthenticated)
        {
            identity = new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, UserName)

                        }, "WebApiAuth");
        }
        else
        {
            identity = new ClaimsIdentity();
        }

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}