using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project.Permissions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Project.Attachments
{
    [Authorize]
    public class AttachmentAppService : ProjectAppService, IAttachmentAppService
    {

        private readonly IAttachmentRepository _attachmentRepository;
        private readonly AttachmentManager _attachmentManager;
        [Obsolete]
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        [Obsolete]
        public AttachmentAppService(
            IAttachmentRepository attachmentRepository,
            AttachmentManager attachmentManager,
            IHostingEnvironment environment,
            IConfiguration configuration)
        {
            _attachmentRepository = attachmentRepository;
            _attachmentManager = attachmentManager;
            _environment = environment;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public async Task<AttachmentDto> GetAsync(Guid id)
        {
            var attachment = await _attachmentRepository.GetAsync(id);
            return ObjectMapper.Map<Attachment, AttachmentDto>(attachment);
        }
        [AllowAnonymous]
        public async Task<PagedResultDto<AttachmentDto>> GetListAsync(GetAttachmentListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Attachment.CreationTime);
            }
            var categories = await _attachmentRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await _attachmentRepository.CountAsync();

            return new PagedResultDto<AttachmentDto>(
                totalCount,
                ObjectMapper.Map<List<Attachment>, List<AttachmentDto>>(categories)
            );
        }
        [AllowAnonymous]
        public async Task<PagedResultDto<AttachmentDto>> GetListAsyncByIdTable(Guid idTable)
        {
            IQueryable<Attachment> queryable = await _attachmentRepository.GetQueryableAsync();
            var query = from attachment in queryable
                        where attachment.IDTable == idTable
                        select new { attachment };
            var queryResult = await AsyncExecuter.ToListAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Attachment), idTable);
            }
            var attachmentDtos = queryResult.Select(x =>
            {
                var attachmentDto = ObjectMapper.Map<Attachment, AttachmentDto>(x.attachment);
                return attachmentDto;
            }).ToList();

            var totalCount = attachmentDtos.Count;

            return new PagedResultDto<AttachmentDto>(
                totalCount,
                attachmentDtos
            );
        }

        public async Task<AttachmentDto> CreateAsync([FromForm] CreateAttachmentDto input)
        {
            string uploads = Path.Combine(_environment.WebRootPath, "attachments");
            var generateFileName = Path.GetRandomFileName() + input.File.FileName;
            var filePath = Path.Combine(uploads, generateFileName);
            var url = "/attachments/" + generateFileName;
            if (input.File.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    input.File.CopyTo(fileStream);
                }
            }
            Console.WriteLine(uploads);
            var attachment = await _attachmentManager.CreateAsync(
                url,
                input.IDTable

            );

            await _attachmentRepository.InsertAsync(attachment);

            return ObjectMapper.Map<Attachment, AttachmentDto>(attachment);
        }

        public async Task UpdateAsync(Guid id, [FromForm] UpdateAttachmentDto input)
        {
            if (input.File != null)
            {
                var attachment = await _attachmentRepository.GetAsync(x => x.IDTable == id);
                string uploads = Path.Combine(_environment.WebRootPath, "attachments");
                var generateFileName = Path.GetRandomFileName() + input.File.FileName;
                var filePath = Path.Combine(uploads, generateFileName);
                var url = "/attachments/" + generateFileName;
                if (input.File.Length > 0)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        input.File.CopyTo(fileStream);
                        attachment.URL = url.ToString();
                        await _attachmentRepository.UpdateAsync(attachment);
                    }
                }
            }
            else
            {
                await _attachmentRepository.DeleteAsync(x => x.IDTable == id);
            }

        }
        [AllowAnonymous]
        public async Task<bool> GetCheckAttachmentExistAsync(Guid id)
        {
            var attachment = await _attachmentRepository.FindAsync(x => x.IDTable == id);
            return (attachment != null) ? true : false;
        }
        public async Task DeleteAsync(Guid id)
        {
            await _attachmentRepository.DeleteAsync(id);
        }

        //...SERVICE METHODS WILL COME HERE...
    }
}