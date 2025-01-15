using System.ComponentModel.DataAnnotations;

namespace ColApp.Models
{
    

    public class ResetPasswordModel
    {
        // Verification du mot de passe 
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Le mot de passe doit comporter au moins 8 caractères.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&\-_.~]{8,}$",
ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre, un caractère spécial (@$!%*?&#-_~), et être long de 8 caractères ou plus.")]
        

        public string Password { get; set; }
    }
}



