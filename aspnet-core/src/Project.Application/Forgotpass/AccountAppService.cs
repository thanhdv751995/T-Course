using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Serilog;
using Project.ForgotPass;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;

namespace Project.Forgotpass
{
    public class AccountAppService : ApplicationService
    {
        public const string PasswordResetToken = "PasswordResetToken";
        private readonly IdentityUserManager _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public AccountAppService(
          IdentityUserManager userManager,
          IEmailSender emailSender,
          IConfiguration configuration)
        {
         
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        public async Task ResetPasswordRequest(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            // For security, don't indicate that the user can't be found
            if (user == null) {              
                    //throw new UserFriendlyException("Email bạn nhập không đúng, vui lòng nhập lại.");
                    //return;             
            } 
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

           
           
            try
            {
                //await _emailSender.SendAsync(user.Email, L["ResetPasswordRequest:EmailSubject"], body);
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse(_configuration.GetSection("Settings")["Abp.Mailing.Smtp.UserName"]));
                mail.To.Add(MailboxAddress.Parse(email));
                mail.Subject = "Test Email Subject";
                mail.Body = new TextPart(TextFormat.Html) { Text = "<a href='http://localhost:4200/resetpassword/"+user.Id+ "/" + System.Web.HttpUtility.UrlEncode(resetToken) +"'>Confirm Your Email Here</a>" };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("Settings")["Abp.Mailing.Smtp.UserName"], _configuration.GetSection("Settings")["Abp.Mailing.Smtp.Password"]);
                smtp.Send(mail);
                smtp.Disconnect(true);
            }
            catch (Exception e)
            {
                Log.Error(e, "ResetPasswordRequest failed! Please ensure the default SMTP settings are valid.");
                throw new UserFriendlyException("Unable to process your request, please try again later.");
            }

            // Do this last to avoid saving the token if errors occur above i.e. the message fails to send
            // todo: Instead of using user.ExtraProperties, Implement AbpUser.PasswordResetToken property instead!
            if (user.ExtraProperties.ContainsKey(PasswordResetToken))
                user.ExtraProperties[PasswordResetToken] = resetToken;
            else
                user.ExtraProperties.Add(PasswordResetToken, resetToken);
            await _userManager.UpdateAsync(user);
        }

        // POST /api/account/reset-password
        public virtual async Task ResetPassword(ResetPasswordInput input)
        {
            var user = await _userManager.FindByIdAsync(input.UserId.ToString());


            // Throws an exception if the token is invalid
            (await _userManager.ResetPasswordAsync(user, input.Token, input.NewPassword)).CheckErrors();

            // todo: I would like to automatically confirm the users email after restting their password but.. 
            // can't use 'user.EmailConfirmed = true;' and need email token to confirm the email when using the _userManager.
            // The only way to do it currently is to use 'GenerateChangeEmailTokenAsync'
            //await _userManager.ConfirmEmailAsync(user, await _userManager.GenerateChangeEmailTokenAsync(user, user.Email));

            user.ExtraProperties.Remove(PasswordResetToken);

            await _userManager.UpdateAsync(user);
        }

    }
}
