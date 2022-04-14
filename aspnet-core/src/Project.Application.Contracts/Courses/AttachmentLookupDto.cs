using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Courses
{
    public class AttachmentLookupDto : EntityDto<Guid>
    {
        public string URL { get; set; }
    }
}
