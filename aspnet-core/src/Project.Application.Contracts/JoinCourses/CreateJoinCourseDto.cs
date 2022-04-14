using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.JoinCourses
{
    public class CreateJoinCourseDto
    {
        public Guid IDCourse { get; set; }
        public Guid IDUser { get; set; }
    }
}
