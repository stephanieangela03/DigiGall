using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiGall.Dtos.User
{
    public class CreateUserDto
    {
        public string NamaLengkap { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Asrama { get; set; } = string.Empty;
    }
}