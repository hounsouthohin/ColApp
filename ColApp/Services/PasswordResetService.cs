using ColApp.Interfaces;
namespace ColApp.Services
{
    public class PasswordResetService
    {
        private readonly IEmailSender _emailSender;
        private readonly UserAccountService _userAccountService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public PasswordResetService(IEmailSender emailSender, UserAccountService userAccountService, ITokenService tokenService, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _userAccountService = userAccountService;
            _tokenService = tokenService;
            _configuration = configuration;          
        }

        // Méthode pour envoyer un lien de réinitialisation à l'email de l'utilisateur
        public async Task SendPasswordResetLinkAsync(string email)
        {
            // Vérifier si l'utilisateur existe dans la base de données
            var user = await _userAccountService.GetByUserMail(email);
            if (user != null)
            {
                // Générer un token sécurisé pour la réinitialisation du mot de passe
                var token = _tokenService.GeneratePasswordResetToken(user);

                // Créer un lien de réinitialisation avec ce token
                var baseUrl = _configuration["AppSettings:BaseUrl"];
                var resetLink = $"{baseUrl}/reset-password?token={token}&email={email}";
                // Le corps du message
                var emailBody = $@"
            <html>
                <body>
                    <p>Cliquez sur ce lien pour réinitialiser votre mot de passe :</p>
                    <a href='{resetLink}'>Réinitialiser le mot de passe</a>
                </body>
            </html>";

                // Envoyer l'email avec le lien cliquable en tant que contenu HTML
                await _emailSender.SendEmailAsync(email, "Réinitialisation de mot de passe", emailBody);

                
            }
            else
            {
                throw new InvalidOperationException("Utilisateur non trouvé");
            }
        }

        public async Task ResetPasswordAsync(string token, string newPassword)
        {
            var user = await _userAccountService.GetUserByResetTokenAsync(token);
            if (user != null)
            {
                var isValid = _tokenService.ValidatePasswordResetToken(token,user); 
                if (isValid)
                {
                    user.MotDePasse = _userAccountService.HashPassword(newPassword,user);
                    user.ResetTokenExpires = null;
                    user.PasswordResetToken = null;
                    await _userAccountService.UpdateUserAsync(user);
                }
                else
                {
                    throw new InvalidOperationException("Token invalide ou expiré.");
                }
            }
            else
            {
                throw new InvalidOperationException("Utilisateur non trouvé.");
            }
        }
    }

}
