using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Application.Dtos
{
    public class RegisterAuthDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
