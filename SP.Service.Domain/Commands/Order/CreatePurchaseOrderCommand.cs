using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class CreatePurchaseOrderCommand: CreateOrderCommand
    {
        public int OrderType { get; set; }
        public CreatePurchaseOrderCommand(Guid id, string remark, string accountId, List<string> cartIds, int addressId,int orderType)
            :base(id, remark, accountId, cartIds, addressId)
        {
            this.OrderType = orderType;
        }
    }
}
