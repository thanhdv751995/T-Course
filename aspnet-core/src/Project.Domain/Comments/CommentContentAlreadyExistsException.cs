using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Project.Comments
{
     public class CommentContentAlreadyExistsException : BusinessException
    {
        public CommentContentAlreadyExistsException(string content)
          : base(ProjectDomainErrorCodes.CommentcontentAlreadyExists)
        {
            WithData("content", content);
        }
    }
}
