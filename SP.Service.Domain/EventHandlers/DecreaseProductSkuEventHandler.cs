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
                System.Console.WriteLine("DecreaseProductSkuEvent ShopId=" + handle.ShopId + "  ProductId=" + handle.ProductId+ 
                    " DecStock="+ handle.DecStock+ " OrderId="+ handle.OrderId);
                lock (lockObjSecond)
                {
                    //var response = StockBusiness.GetProductSku(handle.Host, handle.ProductId, handle.ShopId);
                    //if(response.Sku.Count > 0 && response.Sku[0].Stock >= handle.DecStock)
                    //{
                        var result = StockBusiness.DecreaseProductSku(handle.Host,handle.OrderId, handle.AccountId, handle.ProductId, handle.ShopId, handle.DecStock);
                        if (result.Status != 10001)
                        {
                            throw new ProductSkuException(string.Format($"ShopId={handle.ShopId} " +
                                $"ProductId={handle.ProductId} AccountId={handle.AccountId} OrderId={handle.OrderId}" +
                                $"DecStock={handle.DecStock}"));
                        }
                    //}
                    //else
                    //{
                    //    var str = string.Format($" DecreaseProductSkuEvent stock less ShopId={handle.ShopId} " +
                    //                            $"ProductId={handle.ProductId} domaint.Stock={(response.Sku.Count > 0 ? (response.Sku[0]?.Stock ?? 0) : 0)} " +
                    //                             $"DecStock={handle.DecStock}  OrderId={ handle.OrderId}");
                    //    System.Console.WriteLine();
                    //    throw new ProductSkuException(str);
                    //}
                }
            }
        }
        public void Handle(RedoProductSkuEvent handle)
        {
            lock (lockObj)
            {
                System.Console.WriteLine("RedoProductSkuEvent ShopId=" + handle.ShopId + "  ProductId=" + handle.ProductId);
                
                System.Console.WriteLine("RedoProductSkuEvent Stock=" + handle.RedoStock + " Host="+ handle.Host);
                var response = StockBusiness.RedoProductSku(handle.Host, handle.AccountId, handle.ProductId, handle.ShopId, handle.RedoStock);
                System.Console.WriteLine("RedoProductSkuEvent result=" + response.Status);
            }
        }
    }
}
