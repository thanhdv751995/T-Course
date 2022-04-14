using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Users
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
