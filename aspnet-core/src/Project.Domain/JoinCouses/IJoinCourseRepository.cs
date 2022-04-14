using Project.ThamGiaKhoaHocs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Project.JoinCouses
{
    public interface IJoinCourseRepository : IRepository<JoinCourse, Guid>
    {
        Task<List<JoinCourse>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting
        );
    }
}
