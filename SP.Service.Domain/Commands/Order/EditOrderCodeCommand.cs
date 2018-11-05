using Grpc.Service.Core.Domain.Commands;
using SP.Data.Enum;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class EditOrderCodeCommand : Command
    {
        public OrderStatus OrderStatus { get; internal set; }
        public string OrderCode { get; internal set; }
        public OrderPay PayWay { get; internal set; }
        public EditOrderCodeCommand(string orderCode, OrderStatus OrderStatus, OrderPay payWay) : base(KafkaConfig.CommandBusTopicTitle)
        {
            this.OrderCode = orderCode;
            this.OrderStatus = OrderStatus;
            this.PayWay = payWay;
            this.CommandType = CommandType.EditOrderCode;
        }
    }
}
