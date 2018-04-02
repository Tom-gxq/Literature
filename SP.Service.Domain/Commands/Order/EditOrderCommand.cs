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
        public EditOrderCommand(Guid id, OrderStatus OrderStatus)
        {
            base.Id = id;
            this.OrderStatus = OrderStatus;
        }
    }
}
