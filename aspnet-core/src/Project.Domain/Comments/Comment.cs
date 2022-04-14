using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Project.Comments
{
    public class Comment : AuditedAggregateRoot<Guid>
    {
        public string content { get; set; }
        public Guid? IDParent { get; set; }
        public Guid IDLesson { get; set; }
        public Guid IDUser { get; set; }
        private Comment()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Comment(
           Guid id,
           [NotNull] string Content,
           Guid? idParent,
          [NotNull] Guid idLesson,
          [NotNull] Guid idUser)
           : base(id)
        {
            content = Content;
            IDParent = idParent;
            IDLesson = idLesson;
            IDUser = idUser;
        }
        internal Comment ChangeName([NotNull] string commentContent)
        {
            SetName(commentContent);
            return this;
        }

        private void SetName([NotNull] string Content)
        {
            content = Check.NotNullOrWhiteSpace(
                Content,
                nameof(Content)
            );
        }
    }
}
