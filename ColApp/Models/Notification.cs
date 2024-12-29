using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    public partial class Notification
    {
        [Key]
        [Column("idNotifications")]
        public int IdNotifications { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("message")]
        public string Message { get; set; } = null!;
        [Column("vue")]
        public bool Vue { get; set; }
    }
}
