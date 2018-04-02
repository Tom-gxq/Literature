using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAssociatorCommand : Command
    {
        public int Status { get; set; }
        public EditAssociatorCommand(Guid id, int status)
        {
            base.Id = id;
            this.Status = status;
        }
    }
}
