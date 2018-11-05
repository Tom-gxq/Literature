using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class DelAddressCommand : Command
    {
        public int AddressId { get; set; }
        public DelAddressCommand(Guid id, int addressId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.AddressId = addressId;
            this.CommandType = CommandType.DelAddress;
        }
    }
}
