using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Attachments
{
    public class AttachmentDto : EntityDto<Guid>
    {
        public string URL { get; set; }
        public Guid IDTable { get; set; }
    }
}
