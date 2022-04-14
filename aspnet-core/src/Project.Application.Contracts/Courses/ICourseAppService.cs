using Project.Courses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.KhoaHocs
{
    public interface ICourseAppService : IApplicationService
    {
        Task<CourseDto> GetAsync(Guid id);

        Task<PagedResultDto<CourseDto>> GetListAsync(GetCourseListDto input);

        Task<CourseDto> CreateAsync(CreateCourseDto input);

        Task UpdateAsync(Guid id, UpdateCourseDto input);
        Task<bool> GetCheckPhoneNumberExistAsync(string PhoneNumber,Guid IDUser);
        Task DeleteAsync(Guid id);
        Task<ListResultDto<CourseDto>> GetListLessonByIDCourse(Guid ID);
        Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync();
        Task<ListResultDto<UserLookupDto>> GetUserLookupAsync();
        Task<ListResultDto<AttachmentLookupDto>> GetAttachmentLookupAsync();
        Task<ListResultDto<CourseDto>> GetListCourseByCategoryID(Guid ID);
        //Task<CourseDto> GetListRelative(Guid idUser);
    }
}
