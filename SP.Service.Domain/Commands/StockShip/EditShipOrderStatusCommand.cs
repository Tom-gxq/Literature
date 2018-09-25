using Grpc.Service.Core.Domain.Commands;
using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.StockShip
{
    public class EditShipOrderStatusCommand : Command
    {
        public List<int> ShipOrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public EditShipOrderStatusCommand(List<int> shipOrderId, OrderStatus orderStatus)
        {
            this.ShipOrderId = shipOrderId;
            this.OrderStatus = orderStatus;
        }

    }
}
