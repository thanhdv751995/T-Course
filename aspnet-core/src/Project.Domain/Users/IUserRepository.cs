using Project.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Project.NguoiDungs
{
    public interface IUserRepository : IRepository<AppUser, Guid>
    {
        Task<AppUser> FindByNameAsync(string name);
        Task<AppUser> FindByUsernameAsync(string username);
        Task<AppUser> FindByEmailAsync(string email);

        Task<List<AppUser>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
