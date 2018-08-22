using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class DelProductCommand : Command
    {
        public DelProductCommand(Guid id)
        {
            base.Id = id;
        }
    }
}
