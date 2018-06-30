using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AddShoppingCartNumEventHandler : IEventHandler<AddShoppingCartNumEvent>
    {
        private readonly ShoppingCartReportDatabase _reportDatabase;
        
        public AddShoppingCartNumEventHandler(ShoppingCartReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AddShoppingCartNumEvent handle)
        {
            _reportDatabase.UpdateShoppingCartQuantity(handle.CartId, handle.Quantity);            
        }
    }
}
