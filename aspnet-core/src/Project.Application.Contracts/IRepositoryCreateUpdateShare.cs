using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface IRepositoryCreateUpdateShare<T1,T2> where T1:class
    {
        Task UpdateCategoryAsync(T1 Entity, T2 id);
        Task PostInsertCategoryAsync(T1 Entity);
    }
}
