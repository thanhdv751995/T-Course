using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.PhanQuyens
{
    public class CreateUpdateRoleDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}
