using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiGall.Models;
namespace DigiGall.Dtos.User
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NamaLengkap { get; set; } = string.Empty;
        public string Asrama { get; set; } = string.Empty;
        public decimal SaldoDigigall { get; set; } = 0;
        public string Role { get; set; } = "None";
        public ICollection<Transaksi> Transaksis { get; set; }
        public ICollection<PemberianQuest> PemberianQuests { get; set; }
    }
}