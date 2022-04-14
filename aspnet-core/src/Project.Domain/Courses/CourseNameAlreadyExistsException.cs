using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Project.KhoaHocs
{
    public class CourseNameAlreadyExistsException : BusinessException
    {
        public CourseNameAlreadyExistsException(string name)
            : base(ProjectDomainErrorCodes.CourceNameAlreadyExists)
        {
            WithData("Name", name);
        }
    }
}
