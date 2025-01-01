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
        public string Nom { get; set; } = null!;
        [Column("prenom")]
        [StringLength(50)]
        [Unicode(false)]
        public string Prenom { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Role { get; set; } = null!;
        [Column("courriel")]
        [StringLength(100)]
        [Unicode(false)]
        public string Courriel { get; set; } = null!;
        [Column("motDePasse")]
        [StringLength(100)]
        [Unicode(false)]
        public string MotDePasse { get; set; } = null!;
        [Column("sel")]
        [StringLength(100)]
        [Unicode(false)]
        public string Sel { get; set; } = null!;
        [Column("date_naissance", TypeName = "date")]
        public DateTime? DateNaissance { get; set; }

        [InverseProperty("IdUtilisateurNavigation")]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty("IdUtilisateurNavigation")]
        public virtual ICollection<PhotoUtilisateur> PhotoUtilisateurs { get; set; }
    }
}
