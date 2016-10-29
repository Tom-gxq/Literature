using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Core.DomainModel
{
    public partial class Account : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UserInfo User { get; set; }
    }
}
