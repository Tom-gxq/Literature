using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ProductSkuOrderNumEventHandler : IEventHandler<ProductSkuOrderNumEvent>
    {
        private readonly ProductSkuReportDatabase _reportDatabase;
        private static object lockObj = new object();
        public ProductSkuOrderNumEventHandler(ProductSkuReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(ProductSkuOrderNumEvent handle)
        {
            lock (lockObj)
            {
                var domaint = _reportDatabase.GetProductSkuByProductId(handle.ShopId, handle.ProductId);
                if (domaint != null && !string.IsNullOrEmpty(domaint.SkuId))
                {
                    var result = _reportDatabase.UpdateProductSkuOrderNum(new Entity.ProductSkuEntity()
                    {
                        SkuId = domaint.SkuId,
                        OrderNum = domaint.OrderNum + handle.OrderNum
                    });
                }
            }
        }
    }
}
