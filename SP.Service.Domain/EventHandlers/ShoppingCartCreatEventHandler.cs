using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ShoppingCartCreatEventHandler : IEventHandler<CreatShoppingCartEvent>
    {
        private readonly ShoppingCartReportDatabase _reportDatabase;
        public ShoppingCartCreatEventHandler(ShoppingCartReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(CreatShoppingCartEvent handle)
        {
            var item = new ShoppingCartsEntity()
            {
                AccountId = handle.AccountId,
                CartId = handle.CartId,
                ProductId = handle.ProductId,
                Quantity = handle.Quantity,
                ShopId = handle.ShopId,
                IsEnabled = false,                
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Add(item);
        }
    }
}
