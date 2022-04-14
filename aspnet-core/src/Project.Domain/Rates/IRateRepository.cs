using Project.DanhGias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Project.Rates
{
    public interface IRateRepository : IRepository<Rate, Guid>
    {
        Task<Rate> FindByIDUserAsync(Guid idUser,Guid idCourse);
        Task<List<Rate>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}