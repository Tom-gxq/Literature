using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class DelAddressCommand : Command
    {
        public int AddressId { get; set; }
        public DelAddressCommand(Guid id, int addressId)
        {
            base.Id = id;
            this.AddressId = addressId;
        }
    }
}
