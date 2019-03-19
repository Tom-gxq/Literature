using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class CreateSuppliersProductCommand : SPCommand
    {
        public string AccountId { get; set; }
        public int SuppliersId { get; set; }
        public string ProductId { get; set; }
        public double PurchasePrice { get; set; }
        public int AlertStock { get; set; }
        public CreateSuppliersProductCommand( string accountId,int suppliersId, double purchasePrice, string productId, int stock) 
            : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.AccountId = accountId;
            this.SuppliersId = suppliersId;
            this.ProductId = productId;
            this.AlertStock = stock;
            this.SuppliersId = suppliersId;
            this.PurchasePrice = purchasePrice;
            this.CommandType = CommandType.CreateSuppliersProduct;
        }
    }
}
