using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class EditPurchaseOrderCommand : EditOrderCommand
    {
        public string AccountId { get; set; }
        public EditPurchaseOrderCommand(Guid id, OrderStatus orderStatus, OrderPay payWay,string accountId)
            :base(id, orderStatus, payWay)
        {
            this.AccountId = accountId;
        }
    }
}
