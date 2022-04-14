using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Lessons
{
    public class LessonDto : EntityDto<Guid>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Benefit { get; set; }
        public string CoursesName { get; set; }
        public Guid IDCourse { get; set; }
        public string URL { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
