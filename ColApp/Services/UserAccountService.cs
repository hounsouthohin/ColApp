﻿using Microsoft.EntityFrameworkCore;
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

        public async Task SavePasswordTokenAsync(string token, string mail,DateTime expirationTime)
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

        public async Task SaveEmailTokenAsync(string token, string mail, DateTime expirationTime)
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
                user.VerifyEmailToken = token;
                user.EmailTokenExpiration = expirationTime;

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
                try
                {
                    if (dbContext.Entry(user).State == EntityState.Detached)
                    {
                        dbContext.Attach(user);
                    }

                    user.MotDePasse = utilisateur.MotDePasse;
                    user.ResetTokenExpires = utilisateur.ResetTokenExpires;
                    user.PasswordResetToken = utilisateur.PasswordResetToken;

                    dbContext.Entry(user).State = EntityState.Modified;

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log ou afficher l'erreur
                    Console.WriteLine($"Erreur lors de la mise à jour de l'utilisateur : {ex.Message}");
                }
            }
            else
            {
                throw new InvalidOperationException("Utilisateur non trouvé.");
            }
        }
        public async Task UpdateUserAfterVerificationEmailAsync(Utilisateur utilisateur)
        {
            var dbContext = await _factory.CreateDbContextAsync();
            var user = await GetByUserMail(utilisateur.Courriel);

            if (user != null)
            {
                try
                {
                    if (dbContext.Entry(user).State == EntityState.Detached)
                    {
                        dbContext.Attach(user);
                    }

                    user.IsEmailVerified = utilisateur.IsEmailVerified;//l'utilisateur est verifié avant de l'inscrire
                    user.EmailTokenExpiration = null;
                    user.VerifyEmailToken = null;
                   
                

                    dbContext.Entry(user).State = EntityState.Modified;

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log ou afficher l'erreur
                    Console.WriteLine($"Erreur lors de la mise à jour de l'utilisateur : {ex.Message}");
                }
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


        public async Task<Utilisateur>? GetUserByTokenAsync(string token)
        {   
            var dbContext = await _factory.CreateDbContextAsync();
            var user = dbContext.Utilisateurs
                        .Where(x => x.VerifyEmailToken == token)
                        .FirstOrDefault();
            return user;
        }

        //SUPPRIMER UN UTILISATEUR 
        public async Task DeleteUserAsync(int userId)
        {
            var dbContext = await _factory.CreateDbContextAsync();

            // Rechercher l'utilisateur dans la base de données
            var user = await dbContext.Utilisateurs.FindAsync(userId);

            if (user != null)
            {
                // Supprimer l'utilisateur
                dbContext.Utilisateurs.Remove(user);

                // Sauvegarder les changements
                await dbContext.SaveChangesAsync();
            }
        }


    }


}
