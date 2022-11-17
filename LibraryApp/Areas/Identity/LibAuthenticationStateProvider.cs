using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace LibraryApp.Areas.Identity;

public class LibAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public LibAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionDetails = 
                await _sessionStorage.GetAsync<LibraryUser>("UserSession");
            
            if (!userSessionDetails.Success) 
                return new AuthenticationState(_anonymous);
            
            var userSession = userSessionDetails.Value;
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession?.UserName ?? String.Empty),
                new Claim(ClaimTypes.Role, userSession?.Role.ToString() ?? String.Empty)
            }));
            
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
        
    }

    public async Task UpdateAuthenticationState(LibraryUser libraryUser)
    {
        await _sessionStorage.DeleteAsync("UserSession");
        var claimsPrincipal = _anonymous;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}