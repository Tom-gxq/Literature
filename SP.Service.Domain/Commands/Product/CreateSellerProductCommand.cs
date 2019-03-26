using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class CreateSellerProductCommand : SPCommand
    {
        public string AccountId { get;  set; }
        public int SupplierProductId { get;  set; }
        public CreateSellerProductCommand(Guid id, string accountId, int suppliersId)
            : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.CommandId = id.ToString();
            this.AccountId = accountId;
            this.SupplierProductId = suppliersId;
            this.CommandType = CommandType.CreateSellerProduct;
        }
    }
}
