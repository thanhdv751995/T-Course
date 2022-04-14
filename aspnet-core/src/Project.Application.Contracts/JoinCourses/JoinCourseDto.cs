using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.JoinCourses
{
    public class JoinCourseDto : EntityDto<Guid>
    {
        public Guid IDCourse { get; set; }

        public Guid IDUser { get; set; }
        public string CoursesName { get; set; }
        public string UserName { get; set; }
        public bool HasBeenLock { get; set; }
        public DateTime CreationTime { get; set; }
        public string URL { get; set; }
    }
}
