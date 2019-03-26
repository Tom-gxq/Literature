using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class CreateProductCommand : SPCommand
    {
        public long MainType { get; set; }
        public long SecondType { get; set; }
        public string ProductId { get; set; }
        public string AccountId { get; set; }
        public int SuppliersId { get; set; }
        public double PurchasePrice { get; set; }

        public CreateProductCommand(Guid id, long mainType, long secondType, string productId, 
            string accountId,double purchasePrice, int suppliersId) 
            : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.MainType = mainType;
            this.SecondType = secondType;
            this.ProductId = productId;
            this.AccountId = accountId;
            this.PurchasePrice = purchasePrice;
            this.SuppliersId = suppliersId;
            this.CommandType = CommandType.CreateProduct;
        }
    }
}
