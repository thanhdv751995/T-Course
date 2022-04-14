using Project.EntityFrameworkCore;
using Project.JoinCouses;
using Project.ThamGiaKhoaHocs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Project.JoinCourses
{
    public class EfCoreJoinCourseRepository : EfCoreRepository<ProjectDbContext, JoinCourse, Guid>,
            IJoinCourseRepository
    {
        public EfCoreJoinCourseRepository(
            IDbContextProvider<ProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<JoinCourse>> GetListAsync(int skipCount, int maxResultCount, string sorting)
        {
            {
                var dbSet = await GetDbSetAsync();
                return await dbSet
                    .OrderBy(sorting)
                    .Skip(skipCount)
                    .Take(maxResultCount)
                    .ToListAsync();
            }
        }
    }
}

