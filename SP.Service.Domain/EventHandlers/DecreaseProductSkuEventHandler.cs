using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class DecreaseProductSkuEventHandler: IEventHandler<DecreaseProductSkuEvent>, IEventHandler<RedoProductSkuEvent>
    {
        private readonly ProductSkuReportDatabase _reportDatabase;
        public DecreaseProductSkuEventHandler(ProductSkuReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(DecreaseProductSkuEvent handle)
        {
            System.Console.WriteLine("ProductId=" + handle.ProductId);
            var domaint = _reportDatabase.GetProductSkuByProductId(handle.ProductId);
            if (domaint != null && !string.IsNullOrEmpty(domaint.SkuId))
            {
                System.Console.WriteLine("Stock="+ domaint.Stock);
                var result = _reportDatabase.UpdateProductSkuStock(new Entity.ProductSkuEntity()
                {
                    SkuId = domaint.SkuId,
                    Stock = domaint.Stock - handle.DecStock
                });
                System.Console.WriteLine("result=" + result);
                if (!result)
                {
                    throw new ProductSkuException(string.Format($"ProductId={handle.ProductId} domaint.Stock={domaint.Stock} DecStock={handle.DecStock}"));
                }
            }
        }
        public void Handle(RedoProductSkuEvent handle)
        {
            System.Console.WriteLine("ProductId=" + handle.ProductId);
            var domaint = _reportDatabase.GetProductSkuByProductId(handle.ProductId);
            if (domaint != null && !string.IsNullOrEmpty(domaint.SkuId))
            {
                System.Console.WriteLine("Stock=" + domaint.Stock);
                var result = _reportDatabase.UpdateProductSkuStock(new Entity.ProductSkuEntity()
                {
                    SkuId = domaint.SkuId,
                    Stock = domaint.Stock + handle.RedoStock
                });
                System.Console.WriteLine("result=" + result);
                
            }
        }
    }
}
