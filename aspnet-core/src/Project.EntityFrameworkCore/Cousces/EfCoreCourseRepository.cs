using Microsoft.EntityFrameworkCore;
using Project.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Project.KhoaHocs
{
    public class EfCoreCourseRepository: EfCoreRepository<ProjectDbContext, Course, Guid>,
            ICourseRepository
    {
        public EfCoreCourseRepository(
            IDbContextProvider<ProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
    {
    }

    public async Task<Course> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(course => course.Name == name);
    }
    public async Task<List<Course>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                course => course.Name.Contains(filter)
             )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}
}
