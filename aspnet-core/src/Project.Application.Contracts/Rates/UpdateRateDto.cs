using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Rates
{
    public class UpdateRateDto
    {
        public int RatePoint { get; set; }

        public string Content { get; set; }

        public Guid IDCourse { get; set; }
        public Guid IDUser { get; set; }
    }
}