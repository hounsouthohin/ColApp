using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace ColApp.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage protectedSessionStorage;
        private ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStorage)
        {
            this.protectedSessionStorage = protectedSessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = claimPrincipal;
            try
            {
                var userSessionStorageResult =
                    await protectedSessionStorage.GetAsync<UserSession>("UserSession");
                var userSession =
                    userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession != null)
                {
                   
                    claimsPrincipal =
                        new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userSession.Prenom),
                            new Claim(ClaimTypes.Email,userSession.Courriel),
                            new Claim(ClaimTypes.Role, userSession.Role)
                        }, "CustomAuth"));
                }
            }
            catch
            {
                claimsPrincipal = claimPrincipal;
            }
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await protectedSessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal =
                    new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userSession.Prenom),
                        new Claim(ClaimTypes.Email,userSession.Courriel),
                        new Claim(ClaimTypes.Role, userSession.Role)
                    }, "CustomAuth"));
            }
            else
            {
                await protectedSessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = claimPrincipal;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
