using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }

        public string NamaLengkap { get; set; }
        public string Password { get; set; }
        public string Asrama { get; set; }
        public decimal SaldoDigigall { get; set; }
        public string Role { get; set; }    

        public ICollection<RiwayatTransaksi> RiwayatTransaksis { get; set; }
        public ICollection<Transaksi> Transaksis { get; set; }
        public ICollection<PemberianQuest> PemberianQuests { get; set; }
    }

}
