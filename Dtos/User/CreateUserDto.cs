using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiGall.Dtos.User
{
    public class CreateUserDto
    {
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Asrama { get; set; }
    }
}