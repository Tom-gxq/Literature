using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using StockGRPCInterface;
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
                    var response = StockBusiness.GetProductSku(handle.Host, handle.ProductId, handle.ShopId);
                    if(response.Status == 10001&& response.Sku.Count > 0 && response.Sku[0].Stock > handle.DecStock)
                    {
                        var result = StockBusiness.DecreaseProductSku(handle.Host,handle.OrderId, handle.AccountId, handle.ProductId, handle.ShopId, handle.DecStock);
                        if (result.Status != 10001)
                        {
                            throw new ProductSkuException(string.Format($"ShopId={handle.ShopId} " +
                                $"ProductId={handle.ProductId} domaint.Stock={response.Sku[0].Stock} " +
                                $"DecStock={handle.DecStock}"));
                        }
                    }
                    else
                    {
                        throw new ProductSkuException(string.Format($"ShopId={handle.ShopId} " +
                                                        $"ProductId={handle.ProductId} domaint.Stock={(response.Sku.Count > 0 ? (response.Sku[0]?.Stock?? 0):0)} " +
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
                    System.Console.WriteLine("RedoProductSkuEvent Stock=" + domaint.Stock);
                    var response = StockBusiness.RedoProductSku(handle.Host, handle.AccountId, handle.ProductId, handle.ShopId, handle.RedoStock);
                    System.Console.WriteLine("RedoProductSkuEvent result=" + response.Status);
                }
            }
        }
    }
}
