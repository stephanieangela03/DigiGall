using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Models
{
    public class PemberianQuest
    {
        [Key]
        public int Id { get; set; }

        public DateTime TanggalSelesai { get; set; }
        public string Status { get; set; } = string.Empty;

        [ForeignKey("Quest")]
        public string NamaQuest { get; set; } = string.Empty;
        public Quest? Quest { get; set; }

        [ForeignKey("User")]
        public string Email { get; set; } = string.Empty;
        public List<User>? Users { get; set; }
       

    }
}