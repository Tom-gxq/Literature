using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class SellerProductCreatedHandler: IEventHandler<SellerProductCreatedEvent>, IEventHandler<SellerProductDelEvent>
    {
        private readonly SellerProductReportDatabase _reportDatabase;
        public SellerProductCreatedHandler(SellerProductReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(SellerProductCreatedEvent handle)
        {
            var item = new SellerProductEntity()
            {                
                 AccountId = handle.AccountId,
                 SupplierProductId = handle.SupplierProductId,
                 PreStock = 2
            };

            _reportDatabase.Add(item);
        }

        public void Handle(SellerProductDelEvent handle)
        {
            var item = new SellerProductEntity()
            {
                AccountId = handle.AccountId,
                SupplierProductId = handle.SupplierProductId
            };

            _reportDatabase.Del(item);
        }
    }
}
