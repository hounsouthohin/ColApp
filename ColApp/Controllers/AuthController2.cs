using ColApp.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ColApp.Authentication;

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

                var user = await _userAccountService.GetByUserMail(email);

                if (user == null)
                {
                    return Redirect("/inscription");
                }
                //Creer une session pour l'utilisateur
                
                // Récupérer returnUrl depuis AuthenticationProperties
                var returnUrl = authenticateResult.Properties?.Items.ContainsKey("returnUrl") == true
                    ? authenticateResult.Properties.Items["returnUrl"]
                    : Url.Content("~/"); // Par défaut : page d'accueil

                // Valider que returnUrl est une URL locale
                if (!Url.IsLocalUrl(returnUrl))
                {
                    returnUrl = Url.Content("~/");
                }

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
    