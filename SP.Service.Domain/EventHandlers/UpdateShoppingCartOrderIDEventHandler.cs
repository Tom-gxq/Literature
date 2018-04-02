using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class UpdateShoppingCartOrderIDEventHandler : IEventHandler<UpdateShoppingCartOrderIDEvent>
    {
        private readonly OrderReportDatabase _reportDatabase;
        private object lockObj = new object();
        public UpdateShoppingCartOrderIDEventHandler(OrderReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(UpdateShoppingCartOrderIDEvent handle)
        {
            lock (lockObj)
            {
                var item = new ShoppingCartsEntity()
                {
                    CartId = handle.CartId,
                    OrderId = handle.OrderId
                };

                _reportDatabase.UpdateShoppingCart(item);
            }
        }
    }
}
