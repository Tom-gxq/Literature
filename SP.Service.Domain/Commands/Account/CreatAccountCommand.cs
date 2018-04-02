using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreatAccountCommand: Command
    {
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }

        public CreatAccountCommand(Guid id, string mobilePhone, string email, string password, int status,string userName)
        {
            base.Id = id;
            this.MobilePhone = mobilePhone;
            this.Email = email;
            this.Password = password;
            this.Status = status;
            this.UserName = userName;
        }
    }
}
