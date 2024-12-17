using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace api.Models
{
    public class Transaksi
    {
        [Key]
        public DateTime TanggalTransaksi { get; set; } = DateTime.Now;

        public string AlamatPengiriman { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalHarga { get; set; }
        public int JumlahPembelian { get; set; }

        [ForeignKey("User")]
        public string Email { get; set; } = string.Empty;
        public User? User { get; set; } 

        [ForeignKey("Item")]
        public string NamaItem { get; set; } = string.Empty;
        public Item? Item { get; set; }

    }
}