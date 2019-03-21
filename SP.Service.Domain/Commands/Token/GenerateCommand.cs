using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Token
{
    public class GenerateCommand : SPCommand
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public bool Status { get; set; }
        public DateTime CreateTime { get; set; }
        public GenerateCommand() : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.CommandType = CommandType.Generate;
        }
    }
}
