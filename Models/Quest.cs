using System.ComponentModel.DataAnnotations;

namespace DigiGall.Models
{
    public class Quest
    {
        [Key]
        public string NamaQuest { get; set; }

        public string Kriteria { get; set; }
        public string Deskripsi { get; set; }
        public string Reward { get; set; }
        public DateTime Deadline { get; set; }
        
    }

}
