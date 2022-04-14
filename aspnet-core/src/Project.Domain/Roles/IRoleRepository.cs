using Project.DuLieu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Project.PhanQuyens
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        //Task<PhanQuyen> FindByPhanQuyenAsync(string tenquyen);
        Task<List<Role>> GetListAsync(
                int skipCount,
                int maxResultCount,
                string sorting,
                string filter = null
            );
    }
}
