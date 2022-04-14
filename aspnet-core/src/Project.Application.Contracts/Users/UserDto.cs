using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.NguoiDungs
{
    public class UserDto : EntityDto<Guid>
    {
        public Guid IDRole { get; set; }
        public string RoleName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    
    
}
