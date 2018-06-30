using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAccountMobileCommand : Command
    {
        public string Mobile { get; set; }

        public EditAccountMobileCommand(Guid id, string mobile)
        {
            base.Id = id;
            this.Mobile = mobile;
        }
    }
}
