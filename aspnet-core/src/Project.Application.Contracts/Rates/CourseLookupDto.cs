using System;
using Volo.Abp.Application.Dtos;

namespace Project.Rates
{
    public class CourseLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}