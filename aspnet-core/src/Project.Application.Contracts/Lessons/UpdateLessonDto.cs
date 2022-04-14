using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Lessons
{
    public class UpdateLessonDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Url { get; set; }
        public Guid IDCourse { get; set; }
    }
}