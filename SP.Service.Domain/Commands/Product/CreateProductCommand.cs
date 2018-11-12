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
        public string ProductName { get; set; }
        public string SuppliersId { get; set; }
        public double MarketPrice { get; set; }
        public double PurchasePrice { get; set; }
        public string ImagePath { get; set; }
        public double VIPPrice { get; set; }

        public CreateProductCommand(Guid id, long mainType, long secondType, string productName, 
            string suppliersId,double marketPrice,double purchasePrice,string imagePath, double vipPrice) 
            : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.MainType = mainType;
            this.SecondType = secondType;
            this.ProductName = productName;
            this.SuppliersId = suppliersId;
            this.MarketPrice = marketPrice;
            this.PurchasePrice = purchasePrice;
            this.ImagePath = imagePath;
            this.VIPPrice = vipPrice;
            this.CommandType = CommandType.CreateProduct;
        }
    }
}
