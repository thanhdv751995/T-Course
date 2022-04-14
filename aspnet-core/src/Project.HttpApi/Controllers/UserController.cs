using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Attachments;
using Project.HttpClients;
using Project.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
namespace Project.Controllers
{
    [IgnoreAntiforgeryToken]
    //[Route("api/app/user")]
    public class UserController : AbpController
    {
        private readonly IUserAppService _userService;
        private readonly IAccountAppService _accountAppService;
        private readonly IAttachmentAppService _attachmentService;
        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public UserController(IUserAppService userService, IHostingEnvironment environment, IAttachmentAppService attachmentService, IAccountAppService accountAppService)
        {
            _userService = userService;
            _environment = environment;
            _attachmentService = attachmentService;
            _accountAppService = accountAppService;
        }

        #region Register
        [HttpPost("api/app/user/register")]
        public async Task<ActionResult> Register([FromBody] CreateUserDto input)
        {
            var result = await _userService.Register(input);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        #endregion

        #region Login
        [HttpPost("api/app/user/login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto input)
        {
            var result = await _userService.Login(input);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        #endregion
        //[HttpGet("api/app/user/get-async-by-id")]
        //public async Task<ActionResult> GetAsyncById()
        //{
        //    var result = await _userService.GetAsyncById();

        //    return Ok(result);
        //    //else
        //    //{
        //    //    return BadRequest(result);
        //    //}
        //}

        [HttpPut("api/app/user/upload")]
        public async Task<ActionResult> PostFile(CreateAttachmentDto input)
        {
            //Console.WriteLine(input);
            //var data = new CreateAttachmentDto
            //{
            //    IDTable = (Guid)CurrentUser.Id,
            //    File = input
            //};
            var data = new CreateAttachmentDto
            {
                IDTable = (Guid)CurrentUser.Id,
                File = input.File
            };
            var result = await _userService.ChangeProfilePicture(data);
            return Ok(result);
            //var result = _attachmentService.CreateAsync();
            //string uploads = Path.Combine(_environment.WebRootPath, "uploads");

            //if (file.Length > 0)
            //{
            //    var filePath = Path.Combine(uploads, file.FileName);

            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        file.CopyTo(fileStream);
            //    }
            //}
        }

        //[HttpPost("api/app/user/reset-password")]
        //[Obsolete]
        //public async Task<ActionResult> ResetPassword(SendPasswordResetCodeDto input)
        //{
        //    await _accountAppService.SendPasswordResetCodeAsync(new SendPasswordResetCodeDto { Email = "justforlrn@gmail.com", AppName = "Angular" });
        //    return Ok();
        //}

    }
}
