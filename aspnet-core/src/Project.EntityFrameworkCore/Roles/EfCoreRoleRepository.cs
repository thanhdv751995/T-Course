using Microsoft.EntityFrameworkCore;
using Project.DuLieu;
using Project.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Project.PhanQuyens
{
    public class EfCoreRoleRepository : EfCoreRepository<ProjectDbContext, Role, Guid>,
            IRoleRepository
    {
        public EfCoreRoleRepository(
            IDbContextProvider<ProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<List<Role>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    role => role.Name.Contains(filter)
                 )
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
