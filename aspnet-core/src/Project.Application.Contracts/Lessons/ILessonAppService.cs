using Project.Rates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.Lessons
{
    public interface ILessonAppService : IApplicationService
    {
        Task<LessonDto> GetAsync(Guid id);

        Task<PagedResultDto<LessonDto>> GetListAsync(GetLessonListDto input);

        Task<LessonDto> CreateAsync(CreateLessonDto input);

        Task UpdateAsync(Guid id, UpdateLessonDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<CourseLookupDto>> GetCourseLookupAsync();
    }
}
