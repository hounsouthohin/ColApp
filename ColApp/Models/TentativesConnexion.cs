using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Table("TentativesConnexion")]
    public partial class TentativesConnexion
    {
        [Key]
        public int Id { get; set; }
        [StringLength(65)]
        public string Courriel { get; set; } = null!;
        public int Tentatives { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateDeblocage { get; set; }
    }
}
