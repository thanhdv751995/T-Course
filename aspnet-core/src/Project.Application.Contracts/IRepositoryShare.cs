using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project
{
    public interface IRepositoryShare<T1,T2> where T1:class
    {
        Task<T1> GetByIDCategoryAsync(T2 id);
        
        Task DeleteCategoryByIDAsync(T2 id);
        
    }
}

