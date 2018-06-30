using Grpc.Service.Core.Domain.Commands;
using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class EditOrderCommand : Command
    {
        public OrderStatus OrderStatus { get; set; }
        public OrderPay PayWay { get; internal set; }
        public EditOrderCommand(Guid id, OrderStatus OrderStatus, OrderPay payWay)
        {
            base.Id = id;
            this.OrderStatus = OrderStatus;
            this.PayWay = payWay;
        }
    }
}
