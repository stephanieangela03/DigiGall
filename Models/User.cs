using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; } = string.Empty;

        public string NamaLengkap { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Asrama { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SaldoDigigall { get; set; }

        public List<PemberianQuest>? PemberianQuests { get; set; }
        public List<Transaksi>? Transaksis { get; set; }
        public List<RiwayatTransaksi>? RiwayatTransaksis { get; set; }

        

    }
}