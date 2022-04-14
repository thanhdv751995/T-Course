using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.NguoiDungs
{
    public class RoleLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
