using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAccountPwdCommand : Command
    {
        public string Pwd { get; set; }

        public EditAccountPwdCommand(Guid id, string pwd)
        {
            base.Id = id;
            this.Pwd = pwd;
        }
    }
}
