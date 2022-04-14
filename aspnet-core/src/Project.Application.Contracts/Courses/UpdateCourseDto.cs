using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.KhoaHocs
{
    public class UpdateCourseDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Benefit { get; set; }
        public Guid IDCategory { get; set; }
        public Guid IDUser { get; set; }
        public string Url { get; set; }
    }
}