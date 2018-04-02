using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class OrderDelStockCommand : Command
    {

        public DateTime OrderDate { get; set; }
        

        public OrderDelStockCommand(Guid id, string orderDate)
        {
            base.Id = id;
            this.OrderDate = DateTime.Parse(orderDate);
        }
    }
}
