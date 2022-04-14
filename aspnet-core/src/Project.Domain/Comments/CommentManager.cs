using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Project.Comments
{
    public class CommentManager : DomainService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentManager(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<Comment> CreateAsync(
           [NotNull] string Content,
           [NotNull] Guid IDLesson,
           Guid? IDParent,
           [NotNull] Guid IDUser
           )
        {
            Check.NotNullOrWhiteSpace(Content, nameof(Content));
            return new Comment(
                GuidGenerator.Create(),
                Content,
                IDParent,
                IDLesson,            
                IDUser
            );
        }
        public async Task ChangeNameAsync(
           [NotNull] Comment comment,
           [NotNull] string newContent)
        {
            Check.NotNull(comment, nameof(comment));
            Check.NotNullOrWhiteSpace(newContent, nameof(newContent));


            comment.ChangeName(newContent);
        }

    }
}
