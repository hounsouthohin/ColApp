using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Table("PhotoEleve")]
    public partial class PhotoEleve
    {
        [Key]
        [Column("noPhoto")]
        public int NoPhoto { get; set; }
        [Column("sourcePhoto")]
        [StringLength(255)]
        public string SourcePhoto { get; set; } = null!;
        [Column("idEleve")]
        public int IdEleve { get; set; }

        [ForeignKey("IdEleve")]
        [InverseProperty("PhotoEleves")]
        public virtual Eleve IdEleveNavigation { get; set; } = null!;
    }
}
