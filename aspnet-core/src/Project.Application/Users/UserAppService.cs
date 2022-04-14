using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project.Attachments;
using Project.HttpClients;
using Project.NguoiDungs;
using Project.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Project.Users
{
    [RemoteService(IsEnabled = false)]
    public class UserAppService : ProjectAppService, IUserAppService
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly IHttpClientService _httpClient;
        private readonly IAttachmentAppService _attachmentService;

        public UserAppService(IdentityUserManager identityUserManager, IHttpClientService httpClient, IAttachmentAppService attachmentService)
        {
            _identityUserManager = identityUserManager;
            _httpClient = httpClient;
            _attachmentService = attachmentService;
        }

        readonly string connectTokenPath = "connect/token";


        public async Task<ResponseResult> Register(CreateUserDto input)
        {
            DateTime dateTimeNow = DateTime.Now;
            var user = new IdentityUser(GuidGenerator.Create(), input.UserName, input.Email) { };
            user.Name = input.Name;
            user.SetProperty("DateOfBirth", input.DateOfBirth);
            user.SetProperty("Avatar", input.Avatar ?? "default-image");
            user.Name = input.Name;
            if (dateTimeNow <= input.DateOfBirth || (dateTimeNow.Year - input.DateOfBirth.Year) <= 10 ||
                (dateTimeNow.Year - input.DateOfBirth.Year) >= 100 || input.UserName.Length < 5)
            {
                var result = new ResponseResult
                {
                    Success = false,
                    Data = new ErrorMessage("DateIsInValid", "Độ tuổi không phù hợp! Xin kiểm tra lại!")
                };
                return result;
            }
            Microsoft.AspNetCore.Identity.IdentityResult res = await _identityUserManager.CreateAsync(user, input.Password);
            if (res.Succeeded)
            {
                var result = new ResponseResult
                {
                    Success = true,
                    Data = ObjectMapper.Map<IdentityUser, IdentityUserDto>(user)
                };
                return result;
            }
            else
            {
                var result = new ResponseResult
                {
                    Success = false,
                    Data = new ErrorMessage(res.Errors)
                };
                return result;
            }
        }

        public async Task<ResponseResult> Login(LoginUserDto input)
        {
            var payload = new Dictionary<string, string>
                {
                    { "client_id","Project_App" },
                    { "client_secret", "1q2w3e*" },
                    { "username", input.UserName },
                    { "password", input.Password },
                    { "grant_type", "password" }
                };

            return await _httpClient.ResponseTokenModel(payload, connectTokenPath);
        }
        public async Task<ResponseResult> ChangeProfilePicture(CreateAttachmentDto input)
        {
            var res = await _attachmentService.CreateAsync(input);
            IdentityUser user = await _identityUserManager.FindByIdAsync(res.IDTable.ToString());
            Console.WriteLine(res.URL);
            user.SetProperty("Avatar", res.URL);
            var resp = new ResponseResult
            {
                Success = true,
                Data = res
            };
            return resp;
        }

    }
    }