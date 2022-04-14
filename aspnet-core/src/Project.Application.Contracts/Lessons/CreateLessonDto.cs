using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Lessons
{
    public class CreateLessonDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public Guid IDCourse { get; set; }
        public string URL { get; set; }
    }
}