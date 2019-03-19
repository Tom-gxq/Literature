using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class SuppliersEventHandler : IEventHandler<SuppliersProductCreatedEvent>, IEventHandler<SuppliersRegionCreatedEvent>
    {
        private readonly SuppliersReportDatabase _reportDatabase;
        public SuppliersEventHandler(SuppliersReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(SuppliersProductCreatedEvent handle)
        {
            var item = new SuppliersProductEntity()
            {                
                ProductId = handle.ProductId,
                AlertStock = handle.AlertStock,
                PurchasePrice = handle.PurchasePrice,
                SuppliersId = handle.SuppliersId,
                Status = handle.Status,
                CreateTime = DateTime.Now,
            };

            _reportDatabase.AddSuppliersProduct(item);
        }

        public void Handle(SuppliersRegionCreatedEvent handle)
        {
            var item = new SuppliersRegionEntity()
            {
                RegionID = handle.RegionID,
                SuppliersId = handle.SuppliersId,
                CreateTime = DateTime.Now,
            };

            _reportDatabase.AddSuppliersRegion(item);
        }
    }
}
