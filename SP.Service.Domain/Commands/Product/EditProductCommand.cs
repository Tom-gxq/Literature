using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class EditProductCommand : SPCommand
    {
        public string ProductId { get; set; }
        public double PurchasePrice { get; set; }
        public int SuppliersId { get; set; }

        public EditProductCommand(Guid id, string productId, double purchasePrice, int suppliersId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.ProductId = productId;
            this.PurchasePrice = purchasePrice;
            this.SuppliersId = suppliersId;
            this.CommandType = CommandType.EditProduct;
        }
    }
}
