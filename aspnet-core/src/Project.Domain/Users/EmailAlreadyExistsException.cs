using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Project.NguoiDungs
{
    public class EmailAlreadyExistsException: BusinessException
    {
        public EmailAlreadyExistsException(string email)
            : base(ProjectDomainErrorCodes.EmailAlreadyExists)
        {
            WithData("Email", email);
        }
    }
}
