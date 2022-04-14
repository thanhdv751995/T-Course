using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Comments
{
    public class LessonLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
