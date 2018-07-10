using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class PayPwdModel
    {
        public string AccountId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PrePassword { get; set; }
    }
}
