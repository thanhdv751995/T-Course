using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Project.ForgotPass
{
    public interface IAccountAppService : IApplicationService
    {
        Task ResetPassword(ResetPasswordInput input);
    }
}
