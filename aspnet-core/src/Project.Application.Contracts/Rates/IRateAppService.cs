using Project.KhoaHocs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.Rates
{
    public interface IRateAppService : IApplicationService
    {
        Task<RateDto> GetAsync(Guid id);

        Task<PagedResultDto<RateDto>> GetListAsync(GetRateListDto input);

        Task<RateDto> CreateAsync(CreateRateDto input);
        Task<bool> GetExistRate(Guid IdCourse, Guid IdUser);

        Task UpdateAsync(Guid id, UpdateRateDto input);
        int GetCount();
        int GetUserCount();
        Task DeleteAsync(Guid id);
        Task<ListResultDto<RateDto>> GetListRateByIDCourse(Guid ID, GetRateListDto input);

        Task<ListResultDto<CourseLookupDto>> GetCourseLookupAsync();

        Task<ListResultDto<UserLookupDto>> GetUserLookupAsync();
    }
}