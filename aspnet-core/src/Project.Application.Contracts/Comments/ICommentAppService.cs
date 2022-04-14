using Project.KhoaHocs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.Comments
{
    public interface ICommentAppService : IApplicationService
    {
        Task<CommentDto> GetAsync(Guid id);

        Task<PagedResultDto<CommentDto>> GetListAsync(GetCommentListDto input);

        Task<CommentDto> CreateAsync(CreateCommentDto input);
        int GetCount();
        Task UpdateAsync(Guid id, UpdateCommentDto input);

        Task DeleteAsync(Guid id);
        Task<ListResultDto<LessonLookupDto>> GetLessonLookupAsync();
        Task<ListResultDto<UserLookupDto>> GetUserLookupAsync();
        Task<ListResultDto<CommentDto>> GetListCommentByIDlesson(GetCommentListDto input,Guid ID);
        Task<ListResultDto<CommentDto>> GetListIDChildByIDPrent(Guid ID);
    }
}
