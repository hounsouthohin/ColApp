using ColApp.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ColApp.Authentication;
using System.Text.Json;


namespace ColApp.Controllers
{
    [Route("controller")]
    [ApiController]
    public class AuthController2 : ControllerBase
    {
        private readonly UserAccountService _userAccountService;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public AuthController2(UserAccountService userAccountService, CustomAuthenticationStateProvider authStateProvider)
        {
            _userAccountService = userAccountService;
            _authStateProvider = authStateProvider;
        }


        [HttpGet("signin-google")]
        [AllowAnonymous]//Permettre aux  utilisateurs non authentifiés d'avoir access a cette fonction
        public IActionResult SignInGoogle([FromQuery] string returnUrl)//returnUrl indique ou l'utilisateur est redirige en cas d'authentification reussie
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Content("~/"); // Défaut : page d'accueil
            }

            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("SignInGoogleCallback", "AuthController2"), // Callback après authentification
            

                Items =
        {
            { "returnUrl", returnUrl },
            // Inclure le returnUrl sécurisé
        },
                AllowRefresh = true,
            };
            // Ajouter le paramètre "prompt=select_account" pour forcer le choix du compte
            properties.Items.Add("prompt", "select_account");
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);//Redirige l'utilisateur vers un fournisseur d'authentification google
        }

        [HttpGet("signin-google-callback")]
        public async Task<IActionResult> SignInGoogleCallback()
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
                    return Redirect("/inscription");
                }

                // Générez une clé temporaire pour la session
                var sessionKey = Guid.NewGuid().ToString();

                // Stockez les informations utilisateur associées à cette clé (par exemple, dans la mémoire ou une base de données)
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
                Console.WriteLine($"Erreur dans SignInGoogleCallback : {ex.Message}");
                return Redirect("/erreur");
            }
        }



    }
}
    