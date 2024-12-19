using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class Item
    {
        [Key]
        public string NamaItem { get; set; }

        public string Deskripsi { get; set; }
        public string URLGambar { get; set; }
        public int Stok { get; set; }
        public decimal Harga { get; set; }

        public ICollection<Transaksi>? Transaksis { get; set; }
    }

}
