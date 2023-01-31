using System.Security.Claims;
using LibraryApp.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MongoDB.Bson;

namespace LibraryApp.Identity;

public class LibAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public LibAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionDetails =
                await _sessionStorage.GetAsync<LibraryUser>(nameof(LibraryUser));

            if (!userSessionDetails.Success)
                return new AuthenticationState(_anonymous);

            var userSession = userSessionDetails.Value;
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new (ClaimTypes.Name, userSession?.UserName ?? String.Empty),
                new (ClaimTypes.Role, userSession?.Role.ToString() ?? String.Empty),
                new (ClaimTypes.NameIdentifier, userSession?.Id.ToString() ?? String.Empty)
            }, "LibUserAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }

    }

    public async Task UpdateAuthenticationState(LibraryUser? libraryUser)
    {
        ClaimsPrincipal claimsPrincipal;

        if (libraryUser is null)
        {
            await _sessionStorage.DeleteAsync(nameof(LibraryUser));
            claimsPrincipal = _anonymous;
        }
        else
        {
            await _sessionStorage.SetAsync(nameof(LibraryUser), libraryUser);

            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new (ClaimTypes.Name, libraryUser.UserName ?? String.Empty),
                new (ClaimTypes.Role, libraryUser.Role.ToString()),
                new (ClaimTypes.NameIdentifier, libraryUser.Id.ToString())
            }, "LibUserAuth"));
        }


        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}