using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiGall.Models
{
    public class Quest
    {
        [Key]
        [Required]
        public Guid QuestId { get; set; } = Guid.NewGuid();
        [Required]
        public string NamaQuest { get; set; } = string.Empty;

        public string? Kriteria { get; set; } = string.Empty;
        public string? Deskripsi { get; set; } = string.Empty;
        [Required]
        public int Reward { get; set; } = 0;

        public string? Creator { get; set; } = string.Empty;

        public DateTime Deadline { get; set; } = System.DateTime.Now;

    }

}
