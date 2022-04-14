using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Lessons
{
    public class GetLessonListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
