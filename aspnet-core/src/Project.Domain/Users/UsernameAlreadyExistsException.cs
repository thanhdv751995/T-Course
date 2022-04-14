using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Project.NguoiDungs
{
    public class UsernameAlreadyExistsException : BusinessException
    {
        public UsernameAlreadyExistsException(string username)
           : base(ProjectDomainErrorCodes.UsernameAlreadyExists)
        {
            WithData("UserName", username);
        }
    }
}
