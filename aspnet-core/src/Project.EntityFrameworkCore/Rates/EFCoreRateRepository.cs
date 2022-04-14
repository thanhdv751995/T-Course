using Project.DanhGias;
using Project.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Project.Rates
{
    public class EFCoreRateRepository : EfCoreRepository<ProjectDbContext, Rate, Guid>,
            IRateRepository
    {
        public EFCoreRateRepository(
            IDbContextProvider<ProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Rate> FindByIDUserAsync(Guid idUser,Guid idCourse)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(rate => rate.IDUser == idUser&&rate.IDCourse==idCourse);
        }
        public async Task<List<Rate>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    rate => rate.Content.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}