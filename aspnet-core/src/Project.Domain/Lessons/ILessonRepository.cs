using Project.BaiHocs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Project.Lessons
{
    public interface ILessonRepository : IRepository<Lesson, Guid>
    {
        Task<Lesson> FindByNameAsync(string name);

        Task<List<Lesson>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
