using ColApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ColApp.Authentication;
using System.Text.Json;

namespace ColApp.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController1 : ControllerBase
    {
        private readonly UserAccountService _userAccountService;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public AuthController1(UserAccountService userAccountService, CustomAuthenticationStateProvider authStateProvider)
        {
            _userAccountService = userAccountService;
            _authStateProvider = authStateProvider;
        }

        // Connexion avec Facebook
        [HttpGet("signin-facebook")]
        [AllowAnonymous]
        public IActionResult SignInFacebook([FromQuery] string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Content("~/"); // Redirige vers la page d'accueil par défaut
            }

            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("SignInFacebookCallback", "AuthController1"), // Callback après succès
                Items = { { "returnUrl", returnUrl } },
                AllowRefresh = true
            };

            return Challenge(properties, FacebookDefaults.AuthenticationScheme); // Lancer le flux Facebook
        }

        // Callback après authentification réussie
        [HttpGet("signin-facebook-callback")]
        public async Task<IActionResult> SignInFacebookCallback()
        {
            try
            {
                var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
                {
                    return Redirect("/connexion");
                }

                var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
                var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);

                if (string.IsNullOrEmpty(email))
                {
                    return Redirect("/connexion");
                }

                // Vérifiez si l'utilisateur existe
                var user = await _userAccountService.GetByUserMail(email);

                if (user == null)
                {
                    return Redirect("/inscription"); // Redirigez les nouveaux utilisateurs vers l'inscription
                }

                // Générez une clé temporaire pour la session
                var sessionKey = Guid.NewGuid().ToString();

                // Stockez les informations utilisateur associées à cette clé
                HttpContext.Session.SetString(sessionKey, JsonSerializer.Serialize(new UserSession
                {
                    Courriel = user.Courriel,
                    Role = user.Role,
                    Prenom = user.Prenom,
                    ExpiresAt = DateTime.UtcNow.AddHours(1)
                }));

                // Redirigez l'utilisateur vers la page d'accueil avec le paramètre sessionKey
                var returnUrl = Url.Content($"~/?sessionKey={sessionKey}");
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans SignInFacebookCallback : {ex.Message}");
                return Redirect("/erreur");
            }
        }
    }
}

