using Grpc.Service.Core.Domain.Handlers;
using SP.Api.Model.Product;
using SP.Data.Enum;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using StockGRPCInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ShipOrderCreatedEventHandler: IEventHandler<ShipOrderCreatedEvent>,IEventHandler<ProductSkuEditEvent>,
        IEventHandler<ShipOrderEditEvent>,IEventHandler<EditShipOrderStatusEvent>
    {
        private readonly ShipOrderReportDatabase _reportDatabase;
        private readonly OrderReportDatabase _orderReportDatabase;
        private readonly ShopReportDatabase _shopReportDatabase;
        private readonly AccountProductReportDatabase _accountProductReportDatabase;
        private readonly SellerShipOrderReportDatabase _sellerShipOrderReportDatabase;
        private static object lockObj = new object();
        private static object lockObjSecond = new object();
        public ShipOrderCreatedEventHandler(ShipOrderReportDatabase reportDatabase, 
            OrderReportDatabase orderReportDatabase, ShopReportDatabase shopReportDatabase,
            AccountProductReportDatabase accountProductReportDatabase, SellerShipOrderReportDatabase sellerShipOrderReportDatabase)
        {
            _reportDatabase = reportDatabase;
            _orderReportDatabase = orderReportDatabase;
            _shopReportDatabase = shopReportDatabase;
            _accountProductReportDatabase = accountProductReportDatabase;
            _sellerShipOrderReportDatabase = sellerShipOrderReportDatabase;
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
        public void Handle(EditShipOrderStatusEvent handle)
        {
            var item = new ShippingOrdersEntity()
            {
                Id = handle.ShipOrderId,
                ShippedDate = DateTime.Now,
                IsShipped = handle.IsShiped,
            };

            _reportDatabase.UpdateShippingOrderStatus(item);

            if (handle.IsShiped)
            {
                var shipOrder = _reportDatabase.GetShippingOrdersById(handle.ShipOrderId);
                if (shipOrder != null)
                {
                    var list = _reportDatabase.GetShippingOrdersByOrderId(shipOrder.OrderId);
                    System.Console.WriteLine($"EditShipOrderStatusEvent GetShippingOrdersByOrderId {list?.Count??0}");
                    if (list == null || list.Count == 0)
                    {
                        var order = new OrdersEntity()
                        {
                            OrderId = shipOrder.OrderId,
                            OrderStatus = (int)OrderStatus.Success,
                            UpdateTime = DateTime.Now,
                        };
                        _orderReportDatabase.UpdateOrderStatus(order);
                    }
                }
                else
                {
                    System.Console.WriteLine($"EditShipOrderStatusEvent GetShippingOrdersByOrderId is 0 ShipOrderId={handle.ShipOrderId}");
                }
            }
        }

        public void Handle(ShipOrderEditEvent handle)
        {
            System.Console.WriteLine($"ShipOrderEditEvent {handle.CommandId} {handle.OrderStatus} {handle.AccountId} {handle.PayWay}");
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
               
                var domaint = _orderReportDatabase.GetOrderByOrderId(handle.AggregateId.ToString());
                if (domaint != null)
                {
                    //支付成功添加商品
                    if (handle.OrderStatus == Data.Enum.OrderStatus.Payed)
                    {
                        lock (lockObjSecond)
                        {
                            _orderReportDatabase.UpdateOrderStatus(item);
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
                                    _sellerShipOrderReportDatabase.Add(new ShipOrderEntity()
                                    {
                                        OrderId = domaint.OrderId,
                                        ProductId = product.ProductId,
                                        ShipId = product.SuppliersId,
                                        ShipTo = domaint.AccountId,
                                        ShipDate = DateTime.Now,
                                        IsShipped = false,
                                        Stock = product.Quantity,
                                        ShopId = 0,
                                    });
                                }
                            }
                        }                        
                    }
                    else if (handle.OrderStatus == Data.Enum.OrderStatus.Success)
                    {
                        lock (lockObjSecond)
                        {
                            if (domaint.Products != null && domaint.Products.Count > 0)
                            {                                
                                foreach (var product in domaint.Products.FindAll(x => x.SuppliersId == handle.AccountId))
                                {
                                    var ship = new ShipOrderEntity()
                                    {
                                        OrderId = domaint.OrderId,
                                        ProductId = product.ProductId,
                                        ShipId = product.SuppliersId,
                                        ShipTo = domaint.AccountId,
                                        ShippedDate = DateTime.Now,
                                        IsShipped = true
                                    };
                                    _sellerShipOrderReportDatabase.UpdateShipOrderStatus(ship);
                                    
                                }
                            }
                            var list = _sellerShipOrderReportDatabase.GetShippingOrdersByOrderId(handle.AggregateId.ToString());
                            
                            if (list == null || list.Count == 0)
                            {
                                _orderReportDatabase.UpdateOrderStatus(item);
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
                var domaint = _orderReportDatabase.GetOrderByOrderId(handle.AggregateId.ToString(), handle.AccountId);
                if (domaint != null)
                {
                    lock (lockObjSecond)
                    {
                        if (domaint.Products != null && domaint.Products.Count > 0)
                        {
                            var shopOwner = _shopReportDatabase.GetShopStatus(domaint.AccountId);
                            var list = new List<ProductSkuModel>();
                            foreach (var product in domaint.Products.FindAll(x=>x.SuppliersId == handle.AccountId))
                            {
                                list.Add(new ProductSkuModel()
                                {
                                    accountId = domaint.AccountId,
                                    productId = product.ProductId,
                                    shopId = shopOwner.ShopId,
                                    stock = product.Quantity,
                                    type = 1,//增加
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
