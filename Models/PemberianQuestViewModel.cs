using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiGall.Models
{
    public class PemberianQuestViewModel
    {
        public string NamaPembuat { get; set; }
        public string NamaQuest { get; set; }
        public string Reward { get; set; }
        public string Deskripsi { get; set; }
        public DateTime Deadline { get; set; }
    }
}