using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class AccountFullInfoModel
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
    }
}
