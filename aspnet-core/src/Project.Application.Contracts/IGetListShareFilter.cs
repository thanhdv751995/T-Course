﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Project
{
    public interface IGetListShareFilter<T1,T2>
    {
        Task<PagedResultDto<T1>> GetAllListCategoryAsync(T2 entity);
    }
}
