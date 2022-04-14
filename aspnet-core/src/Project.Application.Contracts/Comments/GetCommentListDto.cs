using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.Comments
{
    public class GetCommentListDto: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
