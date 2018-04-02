using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain;
using System.Threading.Tasks;
using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.EntityFramework.Repositories;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands;
using SP.Service.Domain.DomainEntity;
using Grpc.Service.Core.Dependency;
using SP.Service.Domain.Reporting;
using System.Linq;
using SP.Service.Domain.DelayQueue;
using Newtonsoft.Json.Linq;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateOrderCommandHandler :
        ICommandHandler<CreateOrderCommand>
    {
        private IDataRepository<OrderDomain> _repository;
        private IDataRepository<ShoppingCartsDomain> _cartRepository;
        private IDataRepository<ProductSkuDomain> _skuRepository;
        private OrderReportDatabase _orderReportDatabase;
        private ProductReportDatabase _productReportDatabase;
        private ShopReportDatabase _shopReportDatabase;
        private ProductSkuReportDatabase _skuReportDatabase;
        private object lockObj = new object();

        public CreateOrderCommandHandler(IDataRepository<OrderDomain> repository, OrderReportDatabase orderReportDatabase, 
            ProductReportDatabase productReportDatabase, ShopReportDatabase shopReportDatabase, ProductSkuReportDatabase skuReportDatabase,
            IDataRepository<ShoppingCartsDomain> cartRepository, IDataRepository<ProductSkuDomain> skuRepository)
        {
            this._repository = repository;
            this._skuRepository = skuRepository;
            _orderReportDatabase = orderReportDatabase;
            _productReportDatabase = productReportDatabase;
            _shopReportDatabase = shopReportDatabase;
            _skuReportDatabase = skuReportDatabase;
            _cartRepository = cartRepository;            
        }

        public void Execute(CreateOrderCommand command)
        {
            lock (lockObj)
            {
                var cartList = new List<ShoppingCartsDomain>();
                var groupList = command.CartIds.Distinct();
                foreach (var item in groupList)
                {
                    var shoppingCart = _orderReportDatabase.GetShoppingCartsById(item);

                    if (shoppingCart != null)
                    {
                        var cartDomain = new ShoppingCartsDomain();
                        cartDomain.SetMemento(shoppingCart);
                        var product = _productReportDatabase.GetProductById(shoppingCart.ProductId);
                        cartDomain.SetMemenProductto(product);
                        if (cartDomain.ShopId > 0)
                        {
                            var shop = _shopReportDatabase.GetShopById(cartDomain.ShopId);
                            if (shop.StartTime != null)
                            {
                                var times = shop.StartTime.Split(':');
                                if (times.Length > 1)
                                {
                                    var hour = times[0];
                                    if (!string.IsNullOrEmpty(hour))
                                    {
                                        var newHour = int.Parse(hour) - 2;
                                        shop.StartTime = string.Format("{0}:{1}", newHour, times[1]);
                                    }
                                }
                            }
                            if ( cartDomain.Product != null && !string.IsNullOrEmpty(cartDomain.Product.ProductId))
                            {
                                var skuDomain = _skuReportDatabase.GetProductSkuByProductId(cartDomain.Product.ProductId);
                                if (!string.IsNullOrEmpty(shoppingCart?.OrderId) && (skuDomain?.Stock ?? 0) < cartDomain.Quantity)
                                {
                                    var order = _orderReportDatabase.GetOrderByOrderId(shoppingCart.OrderId);
                                    if (order != null)
                                    {
                                        double amount = order.Amount;
                                        if(order.Amount > (product.MarketPrice != null ? product.MarketPrice.Value:0))
                                        {
                                            amount = amount - product.MarketPrice.Value;
                                        }
                                        double vipAmount = order.VIPAmount;
                                        if (order.VIPAmount > (product.VIPPrice != null ? product.VIPPrice.Value : 0))
                                        {
                                            vipAmount = vipAmount - product.VIPPrice.Value;
                                        }
                                        cartDomain.DeleteShoppingCart(item, shoppingCart.OrderId, shoppingCart.ProductId, amount, vipAmount);
                                        _cartRepository.Save(cartDomain);
                                    }
                                    continue;
                                }
                                else
                                {
                                    System.Console.WriteLine("EditProductSkuDomainStock Quantity=" + shoppingCart.Quantity);
                                    var sku = new ProductSkuDomain();
                                    sku.EditProductSkuDomainStock(shoppingCart.ProductId, (shoppingCart.Quantity!= null? shoppingCart.Quantity.Value:0));
                                    _skuRepository.Save(sku);
                                }
                            }
                        }
                        cartList.Add(cartDomain);
                    }
                }
                if (cartList.Count > 0)
                {
                    var aggregate = new OrderDomain(command.Id, command.Remark, command.OrderStatus, command.OrderDate, command.AccountId, cartList, command.AddressId);

                    _repository.Save(aggregate);
                    JObject data = new JObject();
                    data.Add("orderId", command.Id.ToString());
                    data.Add("orderDate", command.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    var queue = new SP.Service.Domain.DelayQueue.DelayQueue();
                    queue.Push(data);
                }
                else
                {
                    throw new ProductSkuException("");
                }
            }
        }
    }
}
