using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.PhanQuyens
{
    public class RoleDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

    }
}
