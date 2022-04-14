using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Rates
{
    public class RateDto : EntityDto<Guid>
    {
        public int RatePoint { get; set; }
        public string Content { get; set; }
        public Guid IDCourse { get; set; }
        public string CourseName { get; set; }
        public string UserName { get; set; }
        public Guid IDUser { get; set; }
        public DateTime CreationTime { get; set; }
        public decimal RateAverage { get; set; }
        public decimal RateTotalPoint { get; set; }
        public int RateCount { get; set; }
        public string Avarta { get; set; }
    }
}