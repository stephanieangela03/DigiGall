using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class RiwayatTransaksi
    {
        [Key]
        public int Id { get; set; }

        public string NamaTransaksi { get; set; }
        public string TipeTransaksi { get; set; }
        public decimal TotalHarga { get; set; }
        public string Status { get; set; }
        public string NamaPenerima { get; set; }
        public string NamaPengirim { get; set; }

        public DateTime TanggalTransaksi { get; set; }

        [ForeignKey("User")]
        public string Email { get; set; }
        public User User { get; set; }
    }

}
