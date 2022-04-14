using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Project.DanhMucs
{
    public class CategoryNameAlreadyExistsException : BusinessException
    {
        public CategoryNameAlreadyExistsException(string name)
            : base(ProjectDomainErrorCodes.CategoryNameAlreadyExists)
        {
            WithData("Name", name);
        }
    }
}
