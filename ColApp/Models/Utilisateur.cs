using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Table("Utilisateur")]
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Messages = new HashSet<Message>();
            PhotoUtilisateurs = new HashSet<PhotoUtilisateur>();
        }

        [Key]
        [Column("idUtilisateur")]
        public int IdUtilisateur { get; set; }

        [Column("nom")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Nom { get; set; } // Rendu optionnel pour la connexion

        [Column("prenom")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Prenom { get; set; } // Rendu optionnel pour la connexion

        [StringLength(50)]
        [Unicode(false)]
        public string? Role { get; set; }

        [Column("courriel")]
        [StringLength(100)]
        [Unicode(false)]
        [Required(ErrorMessage = "Le courriel est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format du courriel invalide.")]
        public string Courriel { get; set; } = null!;

        [Column("motDePasse")]
        [StringLength(100)]
        [Unicode(false)]
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        public string MotDePasse { get; set; } = null!; // Maintient comme requis mais sans restrictions supplémentaires

        [Column("sel")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Sel { get; set; }

        [Column("date_naissance")]
        [StringLength(50)]
        [Unicode(false)]
        public string? DateNaissance { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? PasswordResetToken { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ResetTokenExpires { get; set; }

        [InverseProperty("IdUtilisateurNavigation")]
        public virtual ICollection<Message> Messages { get; set; }

        [InverseProperty("IdUtilisateurNavigation")]
        public virtual ICollection<PhotoUtilisateur> PhotoUtilisateurs { get; set; }
    }
}

