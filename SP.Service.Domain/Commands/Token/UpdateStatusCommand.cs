﻿using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Token
{
    public class UpdateStatusCommand : SPCommand
    {
        public string AccountId { get; set; }
        public string AccessToken { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateTime { get; set; }
        public UpdateStatusCommand() : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.CommandType = CommandType.UpdateStatus;
        }
    }
}
