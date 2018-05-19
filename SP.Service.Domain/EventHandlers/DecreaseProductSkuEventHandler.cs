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
        private static object lockObj = new object();
        private static object lockObjSecond = new object();
        public DecreaseProductSkuEventHandler(ProductSkuReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(DecreaseProductSkuEvent handle)
        {
            lock (lockObj)
            {
                System.Console.WriteLine("DecreaseProductSkuEvent ShopId=" + handle.ShopId + "  ProductId=" + handle.ProductId);
                lock (lockObjSecond)
                {
                    var domaint = _reportDatabase.GetProductSkuByProductId(handle.ShopId, handle.ProductId);
                    if (domaint != null && !string.IsNullOrEmpty(domaint.SkuId) && domaint.Stock  > 0&& domaint.Stock >= handle.DecStock)
                    {
                        System.Console.WriteLine("Stock=" + domaint.Stock);
                        var result = _reportDatabase.UpdateProductSkuStock(new Entity.ProductSkuEntity()
                        {
                            SkuId = domaint.SkuId,
                            Stock = domaint.Stock - handle.DecStock
                        });
                        System.Console.WriteLine("result=" + result);
                        if (!result)
                        {
                            throw new ProductSkuException(string.Format($"ShopId={handle.ShopId} " +
                                $"ProductId={handle.ProductId} domaint.Stock={domaint.Stock} " +
                                $"DecStock={handle.DecStock}"));
                        }
                    }
                    else
                    {
                        throw new ProductSkuException(string.Format($"ShopId={handle.ShopId} " +
                                                        $"ProductId={handle.ProductId} domaint.Stock={domaint?.Stock??0} " +
                                                        $"DecStock={handle.DecStock}"));
                    }
                }
            }
        }
        public void Handle(RedoProductSkuEvent handle)
        {
            lock (lockObj)
            {
                System.Console.WriteLine("RedoProductSkuEvent ShopId=" + handle.ShopId + "  ProductId=" + handle.ProductId);
                var domaint = _reportDatabase.GetProductSkuByProductId(handle.ShopId, handle.ProductId);
                if (domaint != null && !string.IsNullOrEmpty(domaint.SkuId))
                {
                    System.Console.WriteLine("Stock=" + domaint.Stock);
                    var result = _reportDatabase.RedoProductSkuStock(new Entity.ProductSkuEntity()
                    {
                        SkuId = domaint.SkuId,
                        Stock = domaint.Stock + handle.RedoStock
                    });
                    System.Console.WriteLine("result=" + result);
                }
            }
        }
    }
}
