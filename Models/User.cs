using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class User 
    {
        [Key]
        [Required]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string NamaLengkap { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Asrama { get; set; } = "None";
        public decimal SaldoDigigall { get; set; } = 0;
        public string Role { get; set; } = "User";  
        public ICollection<Transaksi> Transaksis { get; set; }
        public ICollection<PemberianQuest> PemberianQuests { get; set; }
        
    }

}
