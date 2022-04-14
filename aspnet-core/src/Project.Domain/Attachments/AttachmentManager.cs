using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Project.Attachments
{
    public class AttachmentManager : DomainService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        public AttachmentManager(IAttachmentRepository attachmentReposittory)
        {
            _attachmentRepository = attachmentReposittory;
        }
        public async Task<Attachment> CreateAsync(
            [NotNull] string url,
            [NotNull] Guid idTable
            )
        {
            //var existingAuthor = await _authorRepository.FindByNameAsync(tenFile);
            return new Attachment(
                GuidGenerator.Create(),
                url,
                idTable
            );
        }
    }
}