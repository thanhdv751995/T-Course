using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.DanhMucs
{
    public class CategoryDto
        : EntityDto<Guid>
    {
        public Guid? IDParent { get; set; }
         public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string ParentName { get; set; }
    }
}