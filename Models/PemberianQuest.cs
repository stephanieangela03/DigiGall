using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class PemberianQuest
    {
        [Key]
        public int Id { get; set; }

        public DateTime TanggalSelesai { get; set; }
        public string Status { get; set; }

        [ForeignKey("User")]
        public string Email { get; set; }
        public User User { get; set; }

        [ForeignKey("Quest")]
        public string NamaQuest { get; set; }
        public Quest Quest { get; set; }
    }

}
