using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class CreateSuppliersProductCommand : SPCommand
    {
        public long MainType { get; set; }
        public long SecondType { get; set; }
        public string ProductId { get; set; }
        public string AccountId { get; set; }
        public int SuppliersId { get; set; }
        public double PurchasePrice { get; set; }

        public CreateSuppliersProductCommand(Guid id, string accountId, long mainType, long secondType, string productId, double purchasePrice, int suppliersId) 
            : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.AccountId = accountId;
            this.SuppliersId = suppliersId;
            this.ProductId = productId;
            this.MainType = mainType;
            this.SecondType = secondType;
            this.PurchasePrice = purchasePrice;
            this.CommandType = CommandType.CreateSuppliersProduct;
        }
    }
}
