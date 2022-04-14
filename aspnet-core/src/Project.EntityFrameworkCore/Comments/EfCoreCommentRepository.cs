using Microsoft.EntityFrameworkCore;
using Project.DanhMucs;
using Project.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Project.Comments
{
    public class EfCoreCommentRepository : EfCoreRepository<ProjectDbContext, Comment, Guid>,
         ICommentRepository
    {
        public EfCoreCommentRepository(
          IDbContextProvider<ProjectDbContext> dbContextProvider)
          : base(dbContextProvider)
        {
        }



        public async Task<List<Comment>> GetListAsync(int skipCount, int maxResultCount, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    comment => comment.content.Contains(filter)
                 )
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
