

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

        public ServiceConnexion(IDbContextFactory<BDEtabContext> factory,NavigationManager navigator,CustomAuthenticationStateProvider custom)
        {
            _factory = factory;
            _navigator = navigator;
            _custom = custom;
        }

        

        public async Task<int> VerifierConnexionAsync(string courriel, string motDePasse)
        {
            var dbContext = _factory.CreateDbContextAsync().Result;
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
            int connexion = (int)param[2].Value;
            return connexion;
          
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

