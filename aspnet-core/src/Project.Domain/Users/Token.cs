using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Users
{
    public class Token
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }

    }
    public class TokenError
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
