using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.StockShip
{
    public class CreatShipOrderCommand : SPCommand
    {
        public string OrderId { get; set; }
        public string ShippingId { get; set; }
        public string ShipTo { get; set; }
        public bool IsShipped { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int Stock { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public CreatShipOrderCommand(string orderId, string shippingId, string shipTo, DateTime shippingDate,
            int stock,string productId,int shopId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.OrderId = orderId;
            this.ShippingId = shippingId;
            this.ShipTo = shipTo;
            this.ShippingDate = shippingDate;
            this.Stock = stock;
            this.ProductId = productId;
            this.ShopId = shopId;
            this.CommandType = CommandType.CreatShipOrder;
        }
    }
}
