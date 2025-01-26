

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ColApp.Pages;
using ColApp.Data;
using ColApp.Models;
using ColApp.Services;
using System.Data;
using ColApp.Authentication;
using Microsoft.AspNetCore.Components;



namespace ColApp.Services
{
    public class ServiceConnexion
    {
        public readonly IDbContextFactory<BDEtabContext> _factory;
        //public readonly CustomAuthenticationStateProvider _authState;
        public readonly NavigationManager _navigator;
        public readonly CustomAuthenticationStateProvider _custom;
        private readonly UserAccountService _userAccountService;
        public ServiceConnexion(IDbContextFactory<BDEtabContext> factory, NavigationManager navigator, CustomAuthenticationStateProvider custom, UserAccountService userAccountService)
        {
            _factory = factory;
            _navigator = navigator;
            _custom = custom;
            _userAccountService = userAccountService;
        }



        public async Task<int> VerifierConnexionAsync(string courriel, string motDePasse)
        {
            // Initialisation du contexte
            var dbContext = await _factory.CreateDbContextAsync();

            

            // Récupération des informations de l'utilisateur
            var utilisateur = await dbContext.Utilisateurs
                .Where(x => x.Courriel == courriel)
                .Select(x => new { x.IsEmailVerified })
                .FirstOrDefaultAsync();

            if (utilisateur == null) return -4; // Compte inexistant

            // Vérification de l'état du compte (actif ou non)
            bool isActive = utilisateur.IsEmailVerified ?? false;

            // Vérification des tentatives de connexion
            var tentative = await dbContext.TentativesConnexions
                .FirstOrDefaultAsync(t => t.Courriel == courriel);

            if (tentative != null && tentative.DateDeblocage.HasValue && tentative.DateDeblocage.Value > DateTime.UtcNow)
            {
                return -2; // Compte temporairement bloqué
            }

            // Appel de la procédure stockée pour vérifier les informations d'identification
            var param = new[]
            {
        new SqlParameter("@courriel", SqlDbType.Char) { Value = courriel, Size = 65 },
        new SqlParameter("@motDePasse", SqlDbType.NVarChar) { Value = motDePasse },
        new SqlParameter("@valeurConnexion", SqlDbType.Int) { Direction = ParameterDirection.Output }
    };

            await dbContext.Database.ExecuteSqlRawAsync("EXEC connexion @courriel, @motDePasse, @valeurConnexion OUTPUT", param);

            int connexionResult = (int)param[2].Value;

            // Gestion des erreurs de connexion
            if (connexionResult == -1)
            {
                await GérerTentativesConnexionAsync(dbContext, tentative, courriel);
                return -1; // Mot de passe ou courriel incorrect
            }

            // Si la connexion est réussie
            if (isActive)
            {
                if (tentative != null)
                {
                    dbContext.TentativesConnexions.Remove(tentative);
                    await dbContext.SaveChangesAsync();
                }
                return connexionResult;
            }

            // Si le compte est inactif
            return -3; // Inviter l'utilisateur à vérifier son inscription
        }

        // Méthode pour gérer les tentatives de connexion
        private async Task GérerTentativesConnexionAsync(BDEtabContext dbContext, TentativesConnexion tentative, string courriel)
        {
            if (tentative == null)
            {
                // Créer une nouvelle entrée
                dbContext.TentativesConnexions.Add(new TentativesConnexion
                {
                    Courriel = courriel,
                    Tentatives = 1
                });
            }
            else
            {
                // Incrémenter les tentatives et bloquer si nécessaire
                tentative.Tentatives++;
                if (tentative.Tentatives >= 3)
                {
                    tentative.DateDeblocage = DateTime.UtcNow.AddMinutes(5); // Bloquer pour 5 minutes
                    tentative.Tentatives = 0; // Réinitialiser les tentatives après le blocage
                }
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task logout()
        {
            await _custom.UpdateAuthenticationState(null);
            _navigator.NavigateTo(
                "/Connexion", true
            );
        }

    }
}

