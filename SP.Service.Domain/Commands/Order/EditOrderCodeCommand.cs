using Grpc.Service.Core.Domain.Commands;
using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class EditOrderCodeCommand : Command
    {
        public OrderStatus OrderStatus { get; set; }
        public string OrderCode { get; set; }
        public EditOrderCodeCommand(string orderCode, OrderStatus OrderStatus)
        {
            this.OrderCode = orderCode;
            this.OrderStatus = OrderStatus;
        }
    }
}
