using Microsoft.EntityFrameworkCore;
using ColApp.Data;
using ColApp.Models;
using ColApp.Partials;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.AspNetCore.Identity;

namespace ColApp.Services
{

    public class UserAccountService
    {
        public readonly IDbContextFactory<BDEtabContext> _factory;

        public UserAccountService(IDbContextFactory<BDEtabContext> factory)
        {
            _factory = factory;
        }
        public async Task<Utilisateur>? GetByUserMail(string courriel)
        {
            var dbContext = await _factory.CreateDbContextAsync();
            var user = dbContext.Utilisateurs
                        .Where(x => x.Courriel == courriel)
                        .FirstOrDefault();
            return user;
        }

        public async Task<Utilisateur>? GetUserByResetTokenAsync(string token)
        {
            var dbContext = await _factory.CreateDbContextAsync();
            var user = dbContext.Utilisateurs
                        .Where(x => x.PasswordResetToken == token)
                        .FirstOrDefault();
            return user;
        }

        public async Task SaveTokenAsync(string token, string mail,DateTime expirationTime)
        {
            // Créez un contexte de base de données à partir du DbContextFactory
            var dbContext = await _factory.CreateDbContextAsync();

            // Récupérez l'utilisateur par son adresse e-mail
            var user = await dbContext.Utilisateurs
                                       .FirstOrDefaultAsync(u => u.Courriel == mail); // Utilisez FirstOrDefaultAsync pour une récupération sécurisée

            // Vérifiez si l'utilisateur existe et si l'email correspond
            if (user != null)
            {
                // Affectez le token de réinitialisation et sa date d'expiration
                user.PasswordResetToken = token;
                user.ResetTokenExpires = expirationTime;

                // Mettez à jour l'utilisateur dans la base de données
                dbContext.Utilisateurs.Update(user); // Mettre à jour l'entité si nécessaire

                // Sauvegardez les modifications dans la base de données
                await dbContext.SaveChangesAsync();
            }
            else
            {
                // Si l'utilisateur n'est pas trouvé, vous pouvez gérer ce cas selon vos besoins
                throw new InvalidOperationException("Utilisateur non trouvé pour cet email.");
            }
        }


        public async Task UpdateUserAsync(Utilisateur utilisateur)
        {
            var dbContext = await _factory.CreateDbContextAsync();
            var user = await GetByUserMail(utilisateur.Courriel);
            if (user != null)
            {
                // Mettre à jour les propriétés nécessaires de l'utilisateur
                user.MotDePasse = utilisateur.MotDePasse; // Mettre à jour le mot de passe
                user.ResetTokenExpires = utilisateur.ResetTokenExpires;
                user.PasswordResetToken = utilisateur.PasswordResetToken;
                // Sauvegarder les changements dans la base de données
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Utilisateur non trouvé.");
            }
        }

        public string HashPassword(string password, Utilisateur utilisateur)
        {
            var passwordHasher = new PasswordHasher<Utilisateur>(); // Indique qu'on hash pour un objet Utilisateur
            return passwordHasher.HashPassword(utilisateur, password); // Passer l'utilisateur comme paramètre
        }


    }


}
