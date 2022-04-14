using Microsoft.EntityFrameworkCore;
using Project.BaiHocs;
using Project.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Project.Lessons
{
    public class EfCoreLessonRepository : EfCoreRepository<ProjectDbContext, Lesson, Guid>,
            ILessonRepository
    {
        public EfCoreLessonRepository(
            IDbContextProvider<ProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<Lesson> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(lesson => lesson.Name == name);
        }

        public async Task<List<Lesson>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            {
                var dbSet = await GetDbSetAsync();
                return await dbSet
                    .WhereIf(
                        !filter.IsNullOrWhiteSpace(),
                        leeson => leeson.Name.Contains(filter)
                     )
                    .OrderBy(sorting)
                    .Skip(skipCount)
                    .Take(maxResultCount)
                    .ToListAsync();
            }
        }
    }
}
