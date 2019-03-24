using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Commands;

namespace SP.Service.Domain.Commands.Product
{
    public class DelSellerProductCommand: CreateSellerProductCommand
    {
        public DelSellerProductCommand(Guid id, string accountId, int suppliersId)
            : base(id, accountId, suppliersId)
        {
            base.Id = id;
            this.CommandType = CommandType.DelSellerProduct;
        }
    }
}
