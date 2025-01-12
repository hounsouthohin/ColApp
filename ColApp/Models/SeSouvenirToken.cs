using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColApp.Models
{
    [Keyless]
    public partial class SeSouvenirToken
    {
        public int Id { get; set; }
        [StringLength(256)]
        public string UserEmail { get; set; } = null!;
        [StringLength(512)]
        public string Token { get; set; } = null!;
        public DateTime DateExpiration { get; set; }
    }
}
