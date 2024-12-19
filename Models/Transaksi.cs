using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class Transaksi
    {
        [Key]
        [Required]
        public Guid TransaksiId { get; set; } = Guid.NewGuid();

        [Required]
        public int JumlahPembelian { get; set; } = 0;
        public decimal? TotalHarga { get; set; } = 0;
        [Required]
        public DateTime TanggalTransaksi { get; set; } = System.DateTime.Now;

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Item")]
        public Guid ItemId { get; set; }

    }

}
