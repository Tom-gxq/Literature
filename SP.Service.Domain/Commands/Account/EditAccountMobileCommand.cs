using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAccountMobileCommand : SPCommand
    {
        public string Mobile { get; set; }

        public EditAccountMobileCommand(Guid id, string mobile) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.Mobile = mobile;
            this.CommandType = CommandType.EditAccountMobile;
        }
    }
}
