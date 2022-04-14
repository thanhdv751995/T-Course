using Project.Attachments;
using Project.HttpClients;
using Project.NguoiDungs;
using Project.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Project.Users
{
    public interface IUserAppService : IApplicationService
    {

        Task<ResponseResult> Register(CreateUserDto input);
        Task<ResponseResult> Login(LoginUserDto input);
        Task<ResponseResult> ChangeProfilePicture(CreateAttachmentDto input);
        //Task<ResponseResult> GetAsyncById();
    }
}
