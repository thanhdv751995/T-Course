using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Comments
{
    public class CommentDto : EntityDto<Guid>
    {
        public string Content { get; set; }
        public Guid? IDParent { get; set; }
        public Guid IDLesson { get; set; }
        public string LessonName { get; set; }
        public Guid IDUser { get; set; }
        public string UserName { get; set; }
        public DateTime CreationTime { get; set; }
        public string Avartar { get; set; }
        public string Url { get; set; }
    }
}
