using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Table("Etablissement")]
    public partial class Etablissement
    {
        public Etablissement()
        {
            Classes = new HashSet<Classe>();
            Eleves = new HashSet<Eleve>();
            Messages = new HashSet<Message>();
        }

        [Key]
        [Column("idEtablissement")]
        public int IdEtablissement { get; set; }
        [Column("nomEtab")]
        [StringLength(100)]
        [Unicode(false)]
        public string NomEtab { get; set; } = null!;
        [Column("delaiInscription")]
        public int DelaiInscription { get; set; }
        [Column("contact")]
        [StringLength(50)]
        [Unicode(false)]
        public string Contact { get; set; } = null!;
        [Column("nomProviseur")]
        [StringLength(100)]
        [Unicode(false)]
        public string NomProviseur { get; set; } = null!;
        [Column("prenomProviseur")]
        [StringLength(100)]
        [Unicode(false)]
        public string PrenomProviseur { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Region { get; set; } = null!;
        [Column("dateRentre", TypeName = "date")]
        public DateTime DateRentre { get; set; }
        [Column("province")]
        [StringLength(50)]
        [Unicode(false)]
        public string Province { get; set; } = null!;
        [Column("ville")]
        [StringLength(50)]
        [Unicode(false)]
        public string Ville { get; set; } = null!;
        [Column("secteur")]
        [StringLength(50)]
        [Unicode(false)]
        public string Secteur { get; set; } = null!;

        [InverseProperty("IdEtablissementNavigation")]
        public virtual ICollection<Classe> Classes { get; set; }
        [InverseProperty("IdEtablissementNavigation")]
        public virtual ICollection<Eleve> Eleves { get; set; }
        [InverseProperty("IdEtablissementNavigation")]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
