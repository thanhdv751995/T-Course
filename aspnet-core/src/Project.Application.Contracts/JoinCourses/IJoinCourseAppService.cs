using Project.KhoaHocs;
using Project.Rates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.JoinCourses
{
    public interface IJoinCourseAppService : IApplicationService
    {
        Task<JoinCourseDto> GetAsync(Guid id);
        Task<bool> GetJoinCourseAsync(Guid idUser,Guid idCourse);
        Task<bool> GetCheckEverJoinCourseAsync(Guid idUser);
        Task<bool> GetCheckHasBeenLockJoinCourseAsync(Guid idUser, Guid idCourse);
        Task<PagedResultDto<JoinCourseDto>> GetListUserJoinCourseAsync(Guid IDCourse);
        Task<PagedResultDto<JoinCourseDto>> GetListCourseUserJoinAsync(Guid IDUser);

        Task<JoinCourseDto> CreateAsync(CreateJoinCourseDto input);
        Task UpdateAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<ListResultDto<CourseLookupDto>> GetCourseLookupAsync();
        Task<ListResultDto<UserLookupDto>> GetUserLookupAsync();
    }
}
