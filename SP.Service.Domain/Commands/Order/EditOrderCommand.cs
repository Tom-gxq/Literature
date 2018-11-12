using Grpc.Service.Core.Domain.Commands;
using SP.Data.Enum;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class EditOrderCommand : SPCommand
    {
        public OrderStatus OrderStatus { get; set; }
        public OrderPay PayWay { get; internal set; }
        public EditOrderCommand(Guid id, OrderStatus OrderStatus, OrderPay payWay) : base(KafkaConfig.CommandBusTopicTitle)
        {
            base.Id = id;
            this.OrderStatus = OrderStatus;
            this.PayWay = payWay;
            this.CommandType = CommandType.EditOrder;
        }
    }
}
