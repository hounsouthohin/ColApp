using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Table("Eleve")]
    public partial class Eleve
    {
        [Key]
        [Column("idEleve")]
        public int IdEleve { get; set; }
        [Column("noPv")]
        public int NoPv { get; set; }
        [Column("idEtablissement")]
        public int IdEtablissement { get; set; }
        [Column("idClasse")]
        public int IdClasse { get; set; }
        [Column("nom")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        [Column("prenom")]
        [StringLength(50)]
        [Unicode(false)]
        public string Prenom { get; set; } = null!;
        [Column("date_naissance", TypeName = "date")]
        public DateTime DateNaissance { get; set; }

        [ForeignKey("IdClasse")]
        [InverseProperty("Eleves")]
        public virtual Classe IdClasseNavigation { get; set; } = null!;
        [ForeignKey("IdEtablissement")]
        [InverseProperty("Eleves")]
        public virtual Etablissement IdEtablissementNavigation { get; set; } = null!;
    }
}
