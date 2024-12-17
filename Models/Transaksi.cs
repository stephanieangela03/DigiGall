using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class Transaksi
    {
        [Key]
        public int Id { get; set; }

        public int JumlahPembelian { get; set; }
        public decimal TotalHarga { get; set; }
        public string AlamatPengiriman { get; set; }
        public DateTime TanggalTransaksi { get; set; }

        [ForeignKey("User")]
        public string Email { get; set; }
        public User User { get; set; }

        [ForeignKey("Item")]
        public int NamaItem { get; set; }
        public Item Item { get; set; }
    }

}
