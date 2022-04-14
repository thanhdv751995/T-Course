using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.NguoiDungs
{
    public class GetUserListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
