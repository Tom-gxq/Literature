using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAssociatorCommand : Command
    {
        public int Status { get; set; }
        public EditAssociatorCommand(Guid id, int status) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.Status = status;
            this.CommandType = CommandType.EditAssociator;
        }
    }
}
