using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class DelProductCommand : SPCommand
    {
        public DelProductCommand(Guid id) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.CommandType = CommandType.DelProduct;
        }
    }
}
