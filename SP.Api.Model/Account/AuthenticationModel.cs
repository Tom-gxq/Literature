using SP.Api.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class AuthenticationModel
    {
        public string AccountId { get; set; }
        public string Account { get; set; }
        public AuthType AuthType { get; set; }
        public string Token { get; set; }
        public string VerifyCode { get; set; }
        public AuthStatus Status { get; set; }
    }
}
