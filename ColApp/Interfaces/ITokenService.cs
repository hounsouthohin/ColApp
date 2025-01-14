using ColApp.Partials;
using ColApp.Models;
using ColApp.Services;

namespace ColApp.Interfaces
{
    public interface ITokenService
    {
        string GeneratePasswordResetToken(Utilisateur user);
        string GenerateEmailVerificationToken(Utilisateur user);
        bool ValidatePasswordResetToken(string token, Utilisateur user);
    }

    public class TokenService : ITokenService
    {
        private readonly Dictionary<string, (string Email, DateTime Expiration)> _tokenStore = new();
        private readonly UserAccountService _userAccountService;
       
        public TokenService(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;   
        }

        public string GeneratePasswordResetToken(Utilisateur user)
        {
            // Générer un token sécurisé pour la réinitialisation (par exemple en utilisant GUID)
            var token = Guid.NewGuid().ToString();
            var expirationTime = DateTime.UtcNow.AddHours(1); // Expire après 1 heure

            // Sauvegarder le token et l'email de l'utilisateur
            _tokenStore[token] = (user.Courriel, expirationTime);

            //inserer le token dans la base de donnee
            _userAccountService.SavePasswordTokenAsync(token,user.Courriel,expirationTime);

            return token;
        }

        //FONCTIONALITE DE VERIFICATION PAR EMAIL DE L'INSCRIPTION
        public string GenerateEmailVerificationToken(Utilisateur user)
        {
            // Générer un token unique
            var token = Guid.NewGuid().ToString();
            var expirationTime = DateTime.UtcNow.AddMinutes(1); // Expire après 24 heures
                
            // Sauvegarder le token dans la base de données
            _userAccountService.SaveEmailTokenAsync(token, user.Courriel, expirationTime);

            return token;
        }


        public bool ValidatePasswordResetToken(string token, Utilisateur user)
        {
            if (_tokenStore.ContainsKey(token))
            {
                var tokenData = _tokenStore[token];
                // Vérifier si le token correspond à l'email et n'est pas expiré
                if (tokenData.Email == user.Courriel && tokenData.Expiration >= user.ResetTokenExpires)
                {
                    return true;
                }
            }

            return false;
        }   
    }
}
