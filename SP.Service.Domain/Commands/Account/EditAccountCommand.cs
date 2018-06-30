using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAccountCommand : Command
    {
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EditAccountCommand(Guid id, string mobilePhone, string email, string password)
        {
            base.Id = id;
            this.MobilePhone = mobilePhone;
            this.Email = email;
            this.Password = password;
        }
    }
}
