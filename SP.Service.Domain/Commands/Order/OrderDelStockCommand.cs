using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class OrderDelStockCommand : Command
    {

        public DateTime OrderDate { get; set; }
        

        public OrderDelStockCommand(Guid id, string orderDate) : base(KafkaConfig.CommandBusTopicTitle)
        {
            base.Id = id;
            this.OrderDate = DateTime.Parse(orderDate);
            this.CommandType = CommandType.OrderDelStock;
        }
    }
}
