using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Project.NguoiDungs
{
    public class UserAlreadyExistsException : BusinessException
    {
        public UserAlreadyExistsException(string name)
            : base(ProjectDomainErrorCodes.UserAlreadyExists)
        {
            WithData("Name", name);
        }
    }
}
