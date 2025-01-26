using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    public partial class RendezVou
    {
        [Key]
        [Column("idRendezVous")]
        public int IdRendezVous { get; set; }
        [Column("idEleve")]
        public int IdEleve { get; set; }
        [Column("idDisponibilite")]
        public int IdDisponibilite { get; set; }
        [Column("dateHeureReservation", TypeName = "datetime")]
        public DateTime DateHeureReservation { get; set; }
        [Column("statut")]
        [StringLength(20)]
        [Unicode(false)]
        public string Statut { get; set; } = null!;

        [ForeignKey("IdDisponibilite")]
        [InverseProperty("RendezVous")]
        public virtual Disponibilite IdDisponibiliteNavigation { get; set; } = null!;
        [ForeignKey("IdEleve")]
        [InverseProperty("RendezVous")]
        public virtual Eleve IdEleveNavigation { get; set; } = null!;
    }
}
