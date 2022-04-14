﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Project.KhoaHocs
{
    public class CategoryLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public Guid? IDParent { get; set; }
    }
}
