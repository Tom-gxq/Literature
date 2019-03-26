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
            _reportDatabase.AddSuppliersProduct(new SuppliersProductEntity()
            {
                ProductId = handle.ProductId,
                SuppliersId = handle.SuppliersId,
                PurchasePrice = handle.PurchasePrice,
                SaleStatus = 0,
                AlertStock = 0,
                Status = 0,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            });
        }
        public void Handle(ProductEditEvent handle)
        {
            _reportDatabase.UpdateSuppliersProduct(new SuppliersProductEntity()
            {
                ProductId = handle.ProductId,
                SuppliersId = handle.SuppliersId,
                PurchasePrice = handle.PurchasePrice,
                UpdateTime = DateTime.Now
            });
        }

        public void Handle(SaleStatusEditEvent handle)
        {
            _reportDatabase.UpdateSuppliersProduct(new SuppliersProductEntity()
            {
                ProductId = handle.ProductId,
                SuppliersId = handle.SuppliersId,
                SaleStatus = handle.Status,
                UpdateTime = DateTime.Now
            });
        }
    }
}
