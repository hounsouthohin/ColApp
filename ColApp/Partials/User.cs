using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ColApp.Partials
{
    public class User
    {
        [Column("motDePasse")]
        [StringLength(100)]
        [Unicode(false)]
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        public string MotDePasse { get; set; }

        [Column("nom")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nom { get; set; }

        [Column("prenom")]
        [StringLength(50)]
        [Unicode(false)]
        public string Prenom {  get; set; }

        [Column("courriel")]
        [StringLength(100)]
        [Unicode(false)]
        [Required(ErrorMessage = "Le courriel est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format du courriel invalide.")]
        public string Courriel { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string Role { get; set; }
     
    }
}
