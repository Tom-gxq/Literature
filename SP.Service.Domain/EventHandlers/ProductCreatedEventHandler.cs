using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ProductCreatedEventHandler : IEventHandler<ProductCreatedEvent>, IEventHandler<ProductEditEvent>,
        IEventHandler<SaleStatusEditEvent>
    {
        private readonly ProductReportDatabase _reportDatabase;
        public ProductCreatedEventHandler(ProductReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(ProductCreatedEvent handle)
        {
            _reportDatabase.Add(new ProductEntity()
            {
                ProductId = handle.AggregateId.ToString(),
                TypeId = handle.MainType,
                SecondTypeId = handle.SecondType,
                MarketPrice = handle.MarketPrice,
                ProductName = handle.ProductName,
                SuppliersId = handle.SuppliersId,
                PurchasePrice = handle.PurchasePrice,
                SaleStatus = 0,
                Meta_Keywords = handle.ProductName+"|",
                AddedDate = DateTime.Now,
                VIPPrice = handle.VipPrice
            });
        }
        public void Handle(ProductEditEvent handle)
        {
            _reportDatabase.Update(new ProductEntity()
            {
                ProductId = handle.AggregateId.ToString(),
                MarketPrice = handle.MarketPrice,
                ProductName = handle.ProductName,
                PurchasePrice = handle.PurchasePrice,
                UpdateTime = DateTime.Now
            });
        }

        public void Handle(SaleStatusEditEvent handle)
        {
            _reportDatabase.Update(new ProductEntity()
            {
                ProductId = handle.AggregateId.ToString(),
                SaleStatus = handle.Status,
                UpdateTime = DateTime.Now
            });
        }
    }
}
