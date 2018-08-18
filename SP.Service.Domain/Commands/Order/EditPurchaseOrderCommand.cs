using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Order
{
    public class EditPurchaseOrderCommand : EditOrderCommand
    {
        public EditPurchaseOrderCommand(Guid id, OrderStatus orderStatus, OrderPay payWay)
            :base(id, orderStatus, payWay)
        {
            
        }
    }
}
