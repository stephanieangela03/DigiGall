using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Quest
    {
        [Key]
        public string NamaQuest { get; set; } = string.Empty;

        public string Deskripsi { get; set; } = string.Empty;
        public string Criteria { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public double Reward { get; set; } 
        public DateTime Deadline { get; set; }

        public List<PemberianQuest>? PemberianQuests { get; set; }


    }
}