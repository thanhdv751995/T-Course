using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Project.KhoaHocs
{
    public interface ICourseRepository : IRepository<Course, Guid>
    {
        Task<Course> FindByNameAsync(string name);

        Task<List<Course>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
