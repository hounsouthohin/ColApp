using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace ColApp.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage protectedSessionStorage;
        private readonly ProtectedLocalStorage protectedLocalStorage;

        private ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStorage, ProtectedLocalStorage protectedLocalStorage)
        {
            this.protectedSessionStorage = protectedSessionStorage;
            this.protectedLocalStorage = protectedLocalStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = claimPrincipal;

            try
            {
                // Vérifiez la session dans ProtectedSessionStorage
                var sessionResult = await protectedSessionStorage.GetAsync<UserSession>("UserSession");
                var userSession = sessionResult.Success ? sessionResult.Value : null;

                // Si aucun utilisateur en session, vérifiez ProtectedLocalStorage
                if (userSession == null)
                {
                    var localResult = await protectedLocalStorage.GetAsync<UserSession>("RememberMeSession");
                    userSession = localResult.Success ? localResult.Value : null;
                }

                // Valider l'expiration
                if (userSession != null && userSession.ExpiresAt > DateTime.UtcNow)
                {
                    claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Prenom),
                new Claim(ClaimTypes.Email, userSession.Courriel),
                new Claim(ClaimTypes.Role, userSession.Role)
            }, "CustomAuth"));
                }
                else
                {
                    // Session expirée : supprimer les données
                    await protectedSessionStorage.DeleteAsync("UserSession");
                    await protectedLocalStorage.DeleteAsync("RememberMeSession");
                }
            }
            catch
            {
                claimsPrincipal = claimPrincipal;
            }

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }



        public async Task UpdateAuthenticationState(UserSession userSession, bool rememberMe = false)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                // Durée de la session
                if (rememberMe)
                {
                    userSession.ExpiresAt = DateTime.UtcNow.AddDays(1); // Exemple : session valable 1 jours
                    await protectedLocalStorage.SetAsync("RememberMeSession", userSession);
                }
                else 
                {
                    userSession.ExpiresAt = DateTime.UtcNow.AddHours(1); // Exemple : session valable 1 heure
                    await protectedSessionStorage.SetAsync("UserSession", userSession);
                }

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, userSession.Prenom),
            new Claim(ClaimTypes.Email, userSession.Courriel),
            new Claim(ClaimTypes.Role, userSession.Role)
        }, "CustomAuth"));
            }
            else
            {
                await protectedSessionStorage.DeleteAsync("UserSession");
                await protectedLocalStorage.DeleteAsync("RememberMeSession");

                claimsPrincipal = claimPrincipal;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


        public async Task LogoutAsync()
        {
            await protectedSessionStorage.DeleteAsync("UserSession");
            await protectedLocalStorage.DeleteAsync("RememberMeSession");

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


    }
}
