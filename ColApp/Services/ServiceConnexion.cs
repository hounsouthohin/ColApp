

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

        public ServiceConnexion(IDbContextFactory<BDEtabContext> factory, NavigationManager navigator, CustomAuthenticationStateProvider custom)
        {
            _factory = factory;
            _navigator = navigator;
            _custom = custom;
        }



        public async Task<int> VerifierConnexionAsync(string courriel, string motDePasse)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;


            // Vérifier si le courriel est bloqué
            var tentative = await dbContext.TentativesConnexions
                .FirstOrDefaultAsync(t => t.Courriel == courriel);

            if (tentative != null &&
                tentative.DateDeblocage.HasValue && // tres important pour bloquer un compte
                tentative.DateDeblocage.Value > DateTime.UtcNow)
            {
                return -2; // Compte temporairement bloqué
            }

            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName= "@courriel",
                    SqlDbType=SqlDbType.Char,
                    Size= 65,
                    Direction= ParameterDirection.Input,
                    Value= courriel
                },
                new SqlParameter() {
                    ParameterName="@motDePasse",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction= ParameterDirection.Input,
                    Value=motDePasse
                    },
                new SqlParameter()
                {
                    ParameterName = "@valeurConnexion",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Int,
                }};

            var resultat = await dbContext.Database.ExecuteSqlRawAsync($"EXEC connexion @courriel,@motDePasse,@valeurConnexion OUTPUT", param);
            int connexionResult = (int)param[2].Value;

            // Si connexion échouée
            if (connexionResult == -1)
            {
                if (tentative == null)
                {
                    // Créer une nouvelle entrée pour ce courriel
                    dbContext.TentativesConnexions.Add(new TentativesConnexion
                    {
                        Courriel = courriel,
                        Tentatives = 1
                    });
                }
                else
                {
                    // Incrémenter le nombre de tentatives
                    tentative.Tentatives++;

                    if (tentative.Tentatives >= 3)
                    {
                        // Bloquer le compte pour 5 minutes
                        tentative.DateDeblocage = DateTime.UtcNow.AddMinutes(5);
                        tentative.Tentatives = 0; // Réinitialiser les tentatives après le blocage
                    }
                }

                await dbContext.SaveChangesAsync();
                return -1; // Mot de passe ou courriel incorrect
            }

            // Si connexion réussie, supprimer les tentatives de connexion
            if (tentative != null)
            {
                dbContext.TentativesConnexions.Remove(tentative);
                await dbContext.SaveChangesAsync();
            }

            return connexionResult; // Retourner l'ID de l'utilisateur

        }

        public async Task logout()
        {
            await _custom.UpdateAuthenticationState(null);
            _navigator.NavigateTo(
                "/Connexion", true
            );
        }


        /* public async Task GererTentatives()
         {
             var authState = await _authState.GetAuthenticationStateAsync();
             isAuthenticated = authState.User.Identity.IsAuthenticated;
         }*/
    }
}

