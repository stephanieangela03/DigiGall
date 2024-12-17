using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Item
    {
        [Key]
        public string NamaItem { get; set; } = string.Empty;

        public string Deskripsi { get; set; } = string.Empty;
        public string UrlGambar { get; set; } = string.Empty;
        public int Stok { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Harga { get; set; }

        public List<Transaksi>? Transaksis { get; set; }

        
    }
}