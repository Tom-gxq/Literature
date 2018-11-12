using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class EditProductCommand : SPCommand
    {
        public string ProductName { get; set; }
        public double MarketPrice { get; set; }
        public double PurchasePrice { get; set; }
        public string ImagePath { get; set; }

        public EditProductCommand(Guid id, string productName,double marketPrice, double purchasePrice, string imagePath) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.ProductName = productName;
            this.MarketPrice = marketPrice;
            this.PurchasePrice = purchasePrice;
            this.ImagePath = imagePath;
            this.CommandType = CommandType.EditProduct;
        }
    }
}
