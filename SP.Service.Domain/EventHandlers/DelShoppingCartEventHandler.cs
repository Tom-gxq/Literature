using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class DelShoppingCartEventHandler : IEventHandler<DelShoppingCartEvent>
    {
        private readonly ShoppingCartReportDatabase _reportDatabase;
        public DelShoppingCartEventHandler(ShoppingCartReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(DelShoppingCartEvent handle)
        {
            _reportDatabase.RemoveShoppingCart(handle.CartId);
        }
    }
}
