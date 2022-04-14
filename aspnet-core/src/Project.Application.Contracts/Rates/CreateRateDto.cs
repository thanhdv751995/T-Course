using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Rates
{
    public class CreateRateDto
    {
        [Required]
        public int RatePoint { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid IDCourse { get; set; }
        public Guid IDUser { get; set; }
    }
}