using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class RiwayatTransaksi
    {
        [Key]
        public int Id { get; set; }

        public string Mantra { get; set; } = string.Empty;
        public string NamaPenerima { get; set; } = string.Empty;
        public string NamaPengirim { get; set; } = string.Empty;
        public string NamaTransaksi { get; set; } = string.Empty;
        public string TipeTransaksi { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalHarga { get; set; }
        public string Status { get; set; } = string.Empty;

        [ForeignKey("User")]
        public string Email { get; set; } = string.Empty;
        public User? User { get; set; }

        [ForeignKey("Transaksi")]
        public DateTime TanggalTransaksi { get; set; } = DateTime.Now;
        public Transaksi? Transaksi { get; set; }
    }
}