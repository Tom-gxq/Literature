﻿using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateAccountPayPwdCommand : Command
    {
        public string PayPwd { get; set; }

        public CreateAccountPayPwdCommand(Guid id, string payPwd) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.PayPwd = payPwd;
            this.CommandType = CommandType.CreateAccountPayPwd;
        }
    }
}
