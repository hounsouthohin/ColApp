using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Table("Disponibilite")]
    public partial class Disponibilite
    {
        public Disponibilite()
        {
            RendezVous = new HashSet<RendezVou>();
        }

        [Key]
        [Column("idDisponibilite")]
        public int IdDisponibilite { get; set; }
        [Column("idUtilisateur")]
        public int IdUtilisateur { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("heureDebut")]
        public TimeSpan HeureDebut { get; set; }
        [Column("heureFin")]
        public TimeSpan HeureFin { get; set; }
        [Column("statut")]
        [StringLength(20)]
        [Unicode(false)]
        public string Statut { get; set; } = null!;

        [ForeignKey("IdUtilisateur")]
        [InverseProperty("Disponibilites")]
        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;
        [InverseProperty("IdDisponibiliteNavigation")]
        public virtual ICollection<RendezVou> RendezVous { get; set; }
    }
}
