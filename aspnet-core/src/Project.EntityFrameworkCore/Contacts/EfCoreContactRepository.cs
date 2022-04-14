using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Project.Contacts
{
    public class EfCoreContactRepository : EfCoreRepository<ProjectDbContext, Contact, Guid>,
            IContactRepository
    {
        public EfCoreContactRepository(
           IDbContextProvider<ProjectDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public async Task<List<Contact>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}