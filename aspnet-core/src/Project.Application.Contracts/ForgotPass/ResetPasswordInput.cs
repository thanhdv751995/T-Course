using System;
using System.Collections.Generic;
using System.Text;

namespace Project.ForgotPass
{
    public class ResetPasswordInput
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
    public class ResetpasswordUser
    {
        public Guid UserId { get; set; }
        public String NewPassword { get; set; }
    }
}
