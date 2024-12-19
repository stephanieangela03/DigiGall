using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DigiGall.Models
{
    public class PemberianQuest
    {
        [Key] 
        [Required] 
        public Guid PemberianQuestId { get; set; } = Guid.NewGuid();


        [Required] public DateTime? TanggalMulai { get; set; } = System.DateTime.Now;
        [Required] public DateTime? TanggalSelesai { get; set; } = System.DateTime.Now;
        public string Status { get; set; } = string.Empty;

        [ForeignKey("User")] public Guid UserId { get; set; }
        [ForeignKey("Quest")] public Guid QuestId { get; set; }

    }
}
