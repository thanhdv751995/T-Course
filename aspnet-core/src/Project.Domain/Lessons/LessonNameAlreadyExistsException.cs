using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Project.Lessons
{
    public class LessonNameAlreadyExistsException : BusinessException
    {
        public LessonNameAlreadyExistsException(string name)
            : base(ProjectDomainErrorCodes.CourceNameAlreadyExists)
        {
            WithData("Name", name);
        }
    }
}
