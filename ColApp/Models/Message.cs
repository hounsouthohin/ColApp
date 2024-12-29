using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Table("Message")]
    public partial class Message
    {
        [Key]
        [Column("idMessage")]
        public int IdMessage { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("message")]
        public string Message1 { get; set; } = null!;
        [Column("idUtilisateur")]
        public int IdUtilisateur { get; set; }
        [Column("idEtablissement")]
        public int IdEtablissement { get; set; }

        [ForeignKey("IdEtablissement")]
        [InverseProperty("Messages")]
        public virtual Etablissement IdEtablissementNavigation { get; set; } = null!;
        [ForeignKey("IdUtilisateur")]
        [InverseProperty("Messages")]
        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;
    }
}
