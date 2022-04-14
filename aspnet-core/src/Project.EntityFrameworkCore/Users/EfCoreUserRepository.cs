using Microsoft.EntityFrameworkCore;
using Project.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;

namespace Project.NguoiDungs
{
    //public class EfCoreUserRepository : EfCoreRepository<ProjectDbContext, User, Guid>,
    //        IUserRepository
    //{
    //    public EfCoreUserRepository(
    //        IDbContextProvider<ProjectDbContext> dbContextProvider)
    //        : base(dbContextProvider)
    //    {
    //    }

    //    public async Task<User> FindByNameAsync(string name)
    //    {
    //        var dbSet = await GetDbSetAsync();
    //        return await dbSet.FirstOrDefaultAsync(user => user.Name == name);
    //    }
    //    public async Task<User> FindByUsernameAsync(string username)
    //    {
    //        var dbSet = await GetDbSetAsync();
    //        return await dbSet.FirstOrDefaultAsync(user => user.Username == username);
    //    }
    //    public async Task<User> FindByEmailAsync(string email)
    //    {
    //        var dbSet = await GetDbSetAsync();
    //        return await dbSet.FirstOrDefaultAsync(user => user.Email == email);
    //    }
    //    public async Task<List<User>> GetListAsync(
    //        int skipCount,
    //        int maxResultCount,
    //        string sorting,
    //        string filter = null)
    //    {
    //        var dbSet = await GetDbSetAsync();
    //        return await dbSet
    //            .WhereIf(
    //                !filter.IsNullOrWhiteSpace(),
    //                user => user.Name.Contains(filter)
    //             )
    //            .OrderBy(sorting)
    //            .Skip(skipCount)
    //            .Take(maxResultCount)
    //            .ToListAsync();
    //    }
    //}
}
