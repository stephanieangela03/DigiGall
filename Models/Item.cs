using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class Item
    {
        [Key]
        [Required]
        public Guid ItemId { get; set; } = Guid.NewGuid();

        [Required]
        public string NamaItem { get; set; } = string.Empty;

        public string? Deskripsi { get; set; } = string.Empty;
        public string? URLGambar { get; set; } = string.Empty;
        [Required]
        public int Stok { get; set; } = 0;
        [Required]
        public int Harga { get; set; } = 0;

        public ICollection<Transaksi> Transaksis { get; set; }
    }

}
