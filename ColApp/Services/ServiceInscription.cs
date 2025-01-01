using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ColApp.Data;
using ColApp.Models;
using System.Data;
using System.Globalization;

namespace ColApp.Services
{
    public class ServiceInscription
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Courriel { get; set; }
        public string MDP { get; set; }
        public DateTime Date_naissance { get; set; }   
        private IDbContextFactory<BDEtabContext> _BDEtabContext { get; set; }

        public ServiceInscription(IDbContextFactory<BDEtabContext> BDEtabContext)
        {
            Nom = "";
            Prenom = "";
            Courriel = "";
            MDP = "";
            _BDEtabContext = BDEtabContext;
        }

        public bool VerifierCourriel(string email)
        {
            bool utiliser = false;
            var context = _BDEtabContext.CreateDbContextAsync().Result;
            List<string> courriel = (from Utilisateur in context.Utilisateurs
                           where Utilisateur.Courriel == email
                           select Utilisateur.Courriel).ToList();
            
            if (courriel.Count == 1)
            {
                utiliser = true;
            }
            return utiliser;
        }



        public async Task InsererNouveauCompte(string nom, string prenom, DateTime date_naissance, string email, string MDP)
        {
            var dbContext = await _BDEtabContext.CreateDbContextAsync();

            // Convertir la date en chaîne au format dd/MM/yyyy
            string dateFormattee = date_naissance.ToString("dd/MM/yyyy");

            // Déclaration des paramètres avec SqlParameter
            var param1 = new SqlParameter("@nom", SqlDbType.NVarChar) { Value = nom };
            var param2 = new SqlParameter("@prenom", SqlDbType.NVarChar) { Value = prenom };
            var param3 = new SqlParameter("@date_naissance", SqlDbType.NVarChar) { Value = dateFormattee };  // Utiliser NVARCHAR ici
            var param4 = new SqlParameter("@courriel", SqlDbType.NVarChar) { Value = email };
            var param5 = new SqlParameter("@mdp", SqlDbType.NVarChar) { Value = MDP };
            var param6 = new SqlParameter("@role", SqlDbType.NVarChar) { Value = "Utilisateur" };  // Rôle par défaut

            // Exécution de la procédure stockée avec les paramètres
            await dbContext.Database.ExecuteSqlRawAsync(
                "EXEC ajoutUtilisateur @nom, @prenom,@role,@courriel,@date_naissance,@mdp",
                param1, param2, param6, param4, param3, param5);
        }





    }
}

