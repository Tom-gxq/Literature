using Grpc.Service.Core.Domain.Commands;
using SP.Data.Enum;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.StockShip
{
    public class EditShipOrderStatusCommand : SPCommand
    {
        public List<int> ShipOrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public EditShipOrderStatusCommand(List<int> shipOrderId, OrderStatus orderStatus) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.ShipOrderId = shipOrderId;
            this.OrderStatus = orderStatus;
            this.CommandType = CommandType.EditShipOrderStatus;
        }

    }
}
