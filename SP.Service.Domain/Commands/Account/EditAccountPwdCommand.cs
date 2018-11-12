using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAccountPwdCommand : SPCommand
    {
        public string Pwd { get; set; }

        public EditAccountPwdCommand(Guid id, string pwd) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.Pwd = pwd;
            this.CommandType = CommandType.EditAccountPwd;
        }
    }
}
