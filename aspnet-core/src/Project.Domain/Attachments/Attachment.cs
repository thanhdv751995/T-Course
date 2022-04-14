
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.Attachments
{
    public class Attachment : AuditedAggregateRoot<Guid>
    {
        public string URL { get; set; }
        public Guid IDTable { get; set; }


        private Attachment()
        {
        }

        internal Attachment(
            Guid id,
            [NotNull] string url,
            [NotNull] Guid idTable
            ) : base(id)
        {
            URL = url;
            IDTable = idTable;
        }

    }
}