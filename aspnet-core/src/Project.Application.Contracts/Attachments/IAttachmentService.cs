using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.Attachments
{
    public interface IAttachmentAppService : IApplicationService
    {
        Task<AttachmentDto> GetAsync(Guid id);

        Task<PagedResultDto<AttachmentDto>> GetListAsync(GetAttachmentListDto input);

        Task<PagedResultDto<AttachmentDto>> GetListAsyncByIdTable(Guid idTable);

        Task<AttachmentDto> CreateAsync(CreateAttachmentDto input);

        Task UpdateAsync(Guid id, UpdateAttachmentDto input);

        Task DeleteAsync(Guid id);
        Task<bool> GetCheckAttachmentExistAsync(Guid id);
    }
}