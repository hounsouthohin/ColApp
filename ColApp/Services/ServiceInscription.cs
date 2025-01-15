using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ColApp.Data;
using System.Data;
using System.Security.Cryptography;

namespace ColApp.Services
{
    public class ServiceInscription
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractères.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le courriel est obligatoire.")]
        [EmailAddress(ErrorMessage = "Le format du courriel n'est pas valide.")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Le mot de passe doit comporter au moins 8 caractères.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&\-_.~]{8,}$",
        ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre, un caractère spécial (@$!%*?&#-_~), et être long de 8 caractères ou plus.")]
        public string MDP { get; set; }


        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "La date de naissance n'est pas valide.")]
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
        //Penser a mettre une alerte si l'utilisateur existe deja dans la base de donnee : NB
        public async Task InsererNouveauCompte(string nom, string prenom, DateTime date_naissance, string email, string MDP)
        {
            var dbContext = await _BDEtabContext.CreateDbContextAsync();

            string dateFormattee = date_naissance.ToString("dd/MM/yyyy");

            var param1 = new SqlParameter("@nom", SqlDbType.NVarChar) { Value = nom };
            var param2 = new SqlParameter("@prenom", SqlDbType.NVarChar) { Value = prenom };
            var param3 = new SqlParameter("@date_naissance", SqlDbType.NVarChar) { Value = dateFormattee };
            var param4 = new SqlParameter("@courriel", SqlDbType.NVarChar) { Value = email };
            var param5 = new SqlParameter("@mdp", SqlDbType.NVarChar) { Value = MDP };
            var param6 = new SqlParameter("@role", SqlDbType.NVarChar) { Value = "Utilisateur" };

            await dbContext.Database.ExecuteSqlRawAsync(
                "EXEC ajoutUtilisateur @nom, @prenom, @role, @courriel, @date_naissance, @mdp",
                param1, param2, param6, param4, param3, param5);
        }

        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}


