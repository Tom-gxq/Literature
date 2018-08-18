using Grpc.Service.Core.Domain.Handlers;
using SP.Api.Model.Product;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using StockGRPCInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ShipOrderCreatedEventHandler: IEventHandler<ShipOrderCreatedEvent>,IEventHandler<ProductSkuEditEvent>,IEventHandler<ShipOrderEditEvent>
    {
        private readonly ShipOrderReportDatabase _reportDatabase;
        private readonly OrderReportDatabase _orderReportDatabase;
        private readonly ShopReportDatabase _shopReportDatabase;
        private readonly AccountProductReportDatabase _accountProductReportDatabase;
        private static object lockObj = new object();
        private static object lockObjSecond = new object();
        public ShipOrderCreatedEventHandler(ShipOrderReportDatabase reportDatabase, 
            OrderReportDatabase orderReportDatabase, ShopReportDatabase shopReportDatabase,
            AccountProductReportDatabase accountProductReportDatabase)
        {
            _reportDatabase = reportDatabase;
            _orderReportDatabase = orderReportDatabase;
            _shopReportDatabase = shopReportDatabase;
            _accountProductReportDatabase = accountProductReportDatabase;
        }
        public void Handle(ShipOrderCreatedEvent handle)
        {
            var item = new ShippingOrdersEntity()
            {
                OrderId = handle.OrderId,
                ProductId = handle.ProductId,
                ShippingId = handle.ShippingId,
                ShipTo = handle.ShipTo,
                Stock = handle.Stock,
                ShippingDate = handle.ShippingDate,
                ShopId = handle.ShopId,
                IsShipped = false,
            };

            _reportDatabase.Add(item);
        }

        public void Handle(ShipOrderEditEvent handle)
        {
            lock (lockObj)
            {
                var item = new OrdersEntity()
                {
                    OrderId = handle.AggregateId.ToString(),
                    OrderStatus = (int)handle.OrderStatus,
                    UpdateTime = DateTime.Now,
                };
                switch (handle.OrderStatus)
                {
                    case Data.Enum.OrderStatus.Payed:
                        item.PayDate = DateTime.Now;
                        item.IsAliPay = (handle.PayWay == Data.Enum.OrderPay.AliPay);
                        item.IsWxPay = (handle.PayWay == Data.Enum.OrderPay.WxPay);
                        break;
                    case Data.Enum.OrderStatus.Success:
                        item.ShipToDate = DateTime.Now;
                        item.FinishDate = DateTime.Now;
                        break;
                } 
                _orderReportDatabase.UpdateOrderStatus(item);
                //支付成功添加商品
                if (handle.OrderStatus == Data.Enum.OrderStatus.Payed)
                {
                    var domaint = _orderReportDatabase.GetOrderByOrderId(handle.AggregateId.ToString());
                    if (domaint != null)
                    {
                        lock (lockObjSecond)
                        {
                            if (domaint.Products != null && domaint.Products.Count > 0)
                            {
                                var shopOwner = _shopReportDatabase.GetShopStatus(domaint.AccountId);
                                
                                foreach (var product in domaint.Products)
                                {
                                    var entity = _accountProductReportDatabase.GetAccountProduct(domaint.AccountId, product.ProductId, shopOwner.ShopId);
                                    if (entity == null)
                                    {
                                        _accountProductReportDatabase.Add(new AccountProductEntity()
                                        {
                                            AccountId = domaint.AccountId,
                                            ProductId = product.ProductId,
                                            ShopId = shopOwner.ShopId,
                                            Status = 0,
                                            PreStock = 0
                                        });
                                    }
                                    System.Console.WriteLine($"GetShippingOrders OrderId={domaint.OrderId} SuppliersId={product.SuppliersId}");
                                    var shipEntity = _reportDatabase.GetShippingOrders(domaint.OrderId, product.SuppliersId);
                                    if (shipEntity == null)
                                    {
                                        var ship = new ShippingOrdersEntity()
                                        {
                                            OrderId = domaint.OrderId,
                                            ProductId = product.ProductId,
                                            ShippingId = product.SuppliersId,
                                            ShipTo = domaint.AccountId,
                                            ShippingDate = DateTime.Now,
                                            IsShipped = false,
                                            Stock = 0,
                                            ShopId = 0,
                                        };
                                        _reportDatabase.Add(ship);
                                        System.Console.WriteLine($"GetShippingOrders Success");
                                    }
                                    else
                                    {
                                        System.Console.WriteLine($"GetShippingOrders Exsit");
                                    }
                                }
                            }
                            
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 给采购人增加库存
        /// </summary>
        /// <param name="handle"></param>
        public void Handle(ProductSkuEditEvent handle)
        {
            lock (lockObj)
            {
                System.Console.WriteLine("ProductSkuEditEvent OrderId=" + handle.AggregateId.ToString() );
                var domaint = _orderReportDatabase.GetOrderByOrderId(handle.AggregateId.ToString());
                if (domaint != null)
                {
                    lock (lockObjSecond)
                    {
                        if (domaint.Products != null && domaint.Products.Count > 0)
                        {
                            var shopOwner = _shopReportDatabase.GetShopStatus(domaint.AccountId);
                            var list = new List<ProductSkuModel>();
                            foreach (var product in domaint.Products)
                            {                                
                                list.Add(new ProductSkuModel()
                                {
                                    accountId = domaint.AccountId,
                                    productId = product.ProductId,
                                    shopId = shopOwner.ShopId,
                                    stock = product.Quantity
                                });                                
                            }
                            var response = StockBusiness.UpdateProductSku(handle.Host, list);
                        }
                    }
                }
            }
        }
    }
}
