using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.KhoaHocs
{
    public class CourseDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string Benefit { get; set; }
        public string NameUser { get; set; }
        public Guid IDCategory { get; set; }
        public Guid IDUser { get; set; }
        public string URL { get; set; }
        public string LessonName { get; set; }
        public string LessonURL { get; set; }
        public string LessonDescription { get; set; }
        public Guid LessonID { get; set; }
        public DateTime CreationTime { get; set; }
        public int RateCount { get; set; }
        public decimal RateTotalPoint { get; set; }
        public decimal RateAverage { get; set; }
        public int CountStudent { get; set; }
    }
}
