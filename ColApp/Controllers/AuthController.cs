using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ColApp.Services;

namespace ColApp.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UserAccountService _userAccountService;
       
        public AuthController(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        // Cette méthode gère le callback après que Google a authentifié l'utilisateur.
        [HttpGet("signin-google-callback")]
        public async Task<IActionResult> SignInGoogleCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("External");

            if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            {
                // Rediriger vers la page de connexion en cas d'échec d'authentification
                return Redirect("/connexion");
            }

            // Extraire les informations utilisateur
            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);
            var avatar = authenticateResult.Principal.FindFirstValue("urn:google:picture");

            // Rechercher l'utilisateur dans la base de données
            var user = await _userAccountService.GetByUserMail(email);

            if (user == null)
            {
                // Si l'utilisateur n'existe pas, rediriger vers la page d'inscription avec les informations récupérées
                return Redirect($"/inscription");
            }

            // Si l'utilisateur existe, continuer avec la logique de connexion
            return Redirect("/"); // Rediriger vers la page d'accueil
        }

        // Cette méthode initie l'authentification avec Google
        [HttpGet("signin-google")]
        [AllowAnonymous]
        public IActionResult SignInGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("SignInGoogleCallback", "auth"), // Spécifiez la route correcte pour la redirection
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
    }
}

