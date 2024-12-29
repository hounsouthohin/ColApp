using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Table("PhotoUtilisateur")]
    public partial class PhotoUtilisateur
    {
        [Key]
        [Column("noPhoto")]
        public int NoPhoto { get; set; }
        [Column("sourcePhoto")]
        [StringLength(255)]
        public string SourcePhoto { get; set; } = null!;
        [Column("idUtilisateur")]
        public int IdUtilisateur { get; set; }

        [ForeignKey("IdUtilisateur")]
        [InverseProperty("PhotoUtilisateurs")]
        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;
    }
}
