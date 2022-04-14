using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Attachments;
using Project.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Project.Attachments
{
    public class EfCoreAttachmentRepository
        : EfCoreRepository<ProjectDbContext, Attachment, Guid>,
            IAttachmentRepository
    {
        public EfCoreAttachmentRepository(
            IDbContextProvider<ProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


        public async Task<List<Attachment>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
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