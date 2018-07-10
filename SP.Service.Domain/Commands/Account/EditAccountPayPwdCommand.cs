using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAccountPayPwdCommand : Command
    {
        public string PayPwd { get; set; }

        public EditAccountPayPwdCommand(Guid id, string payPwd)
        {
            base.Id = id;
            this.PayPwd = payPwd;
        }
    }
}
