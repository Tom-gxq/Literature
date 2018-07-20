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
using SP.Service.Domain.Commands.Order;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>, ICommandHandler<CreatePurchaseOrderCommand>
    {
        private IDataRepository<OrderDomain> _repository;
        private IDataRepository<ShoppingCartsDomain> _cartRepository;
        private IDataRepository<ProductSkuDomain> _skuRepository;
        private OrderReportDatabase _orderReportDatabase;
        private ProductReportDatabase _productReportDatabase;
        private ShopReportDatabase _shopReportDatabase;
        private ProductSkuReportDatabase _skuReportDatabase;
        private AddressReportDatabase _addressReportDatabase;
        private AccountReportDatabase _accountReportDatabase;
        private AssociatorReportDatabase _associatorReportDatabase;
        private static object lockObj = new object();
        private static object lockObjSecond = new object();
        private static object lockPurchaseObj = new object();
        private static object lockPurchaseObjSecond = new object();


        public CreateOrderCommandHandler(IDataRepository<OrderDomain> repository, OrderReportDatabase orderReportDatabase, 
            ProductReportDatabase productReportDatabase, ShopReportDatabase shopReportDatabase, ProductSkuReportDatabase skuReportDatabase,
            AddressReportDatabase addressReportDatabase,
            IDataRepository<ShoppingCartsDomain> cartRepository, IDataRepository<ProductSkuDomain> skuRepository,
            AccountReportDatabase accountReportDatabase, AssociatorReportDatabase associatorReportDatabase)
        {
            this._repository = repository;
            this._skuRepository = skuRepository;
            _orderReportDatabase = orderReportDatabase;
            _productReportDatabase = productReportDatabase;
            _shopReportDatabase = shopReportDatabase;
            _skuReportDatabase = skuReportDatabase;
            _addressReportDatabase = addressReportDatabase;
            _cartRepository = cartRepository;
            _accountReportDatabase = accountReportDatabase;
            _associatorReportDatabase = associatorReportDatabase;
        }

        public void Execute(CreateOrderCommand command)
        {
            lock (lockObj)
            {
                lock (lockObjSecond)
                {
                    var address = _addressReportDatabase.GetAddressById(command.AddressId, command.AccountId);
                    var cartList = new List<ShoppingCartsDomain>();
                    var groupList = command.CartIds.Distinct();
                    foreach (var item in groupList)
                    {
                        var shoppingCart = _orderReportDatabase.GetShoppingCartsById(item);

                        if (shoppingCart != null && string.IsNullOrEmpty(shoppingCart.OrderId))
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
                                if (cartDomain.Product != null && !string.IsNullOrEmpty(cartDomain.Product.ProductId))
                                {
                                    var skuDomain = _skuReportDatabase.GetProductSkuByProductId(cartDomain.ShopId, cartDomain.Product.ProductId);
                                    if (!string.IsNullOrEmpty(shoppingCart?.OrderId) && (skuDomain?.Stock ?? 0) < cartDomain.Quantity)
                                    {
                                        throw new ProductSkuException(string.Format($"ShopId={skuDomain.ShopId} " +
                                        $"ProductId={skuDomain.ProductId} domaint.Stock={skuDomain.Stock} " +
                                        $"DecStock={cartDomain.Quantity}"));
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("EditProductSkuDomainStock Quantity=" + shoppingCart.Quantity);
                                        var sku = new ProductSkuDomain();
                                        sku.EditProductSkuDomainStock(cartDomain.ShopId, shoppingCart.ProductId, cartDomain.Quantity, command.Id.ToString(), command.AccountId);
                                        _skuRepository.Save(sku);
                                    }
                                }
                            }
                            cartList.Add(cartDomain);
                        }
                    }
                    if (cartList.Count > 0)
                    {
                        var memberList = _associatorReportDatabase.GetMemberByAccountId(command.AccountId);
                        var account = _accountReportDatabase.GetAccountById(command.AccountId);
                        var isvip = (memberList != null && memberList.Count > 0);
                        var aggregate = new OrderDomain(command.Id, command.Remark, command.OrderStatus, command.OrderDate, command.AccountId, cartList, command.AddressId, address.Address, account.MobilePhone, isvip);

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

        public void Execute(CreatePurchaseOrderCommand command)
        {
            lock (lockPurchaseObj)
            {
                lock (lockPurchaseObjSecond)
                {
                    var address = _addressReportDatabase.GetAddressById(command.AddressId, command.AccountId);
                    var cartList = new List<ShoppingCartsDomain>();
                    var groupList = command.CartIds.Distinct();
                    foreach (var item in groupList)
                    {
                        var shoppingCart = _orderReportDatabase.GetShoppingCartsById(item);

                        if (shoppingCart != null && string.IsNullOrEmpty(shoppingCart.OrderId))
                        {
                            var cartDomain = new ShoppingCartsDomain();
                            cartDomain.SetMemento(shoppingCart);
                            var product = _productReportDatabase.GetProductById(shoppingCart.ProductId);
                            cartDomain.SetMemenProductto(product);

                            if (cartDomain.ShopId > 0 && cartDomain.Product != null
                                    && !string.IsNullOrEmpty(cartDomain.Product.ProductId))
                            {
                                //var skuDomain = _skuReportDatabase.GetProductSkuByProductId(cartDomain.ShopId, cartDomain.Product.ProductId);
                                //if (!string.IsNullOrEmpty(shoppingCart?.OrderId) && (skuDomain?.Stock ?? 0) < cartDomain.Quantity)
                                //{
                                //    throw new ProductSkuException(string.Format($"ShopId={skuDomain.ShopId} " +
                                //    $"ProductId={skuDomain.ProductId} domaint.Stock={skuDomain.Stock} " +
                                //    $"DecStock={cartDomain.Quantity}"));
                                //}
                                //else
                                //{
                                //System.Console.WriteLine("EditProductSkuDomainStock Quantity=" + shoppingCart.Quantity);
                                //var sku = new ProductSkuDomain();
                                //sku.EditProductSkuDomainStock(cartDomain.ShopId, shoppingCart.ProductId, cartDomain.Quantity, command.Id.ToString(), command.AccountId);
                                //_skuRepository.Save(sku);
                                //}
                            }

                            cartList.Add(cartDomain);
                        }
                    }
                    if (cartList.Count > 0)
                    {
                        var account = _accountReportDatabase.GetAccountById(command.AccountId);
                        var aggregate = new OrderDomain();
                        aggregate.PurchaseOrderDomain(command.Id, command.Remark, command.OrderStatus, command.OrderDate, command.AccountId, cartList, command.AddressId, address?.Address??string.Empty, account?.MobilePhone??string.Empty);
                        _repository.Save(aggregate);
                        
                    }
                    else
                    {
                        throw new ProductSkuException("");
                    }
                }
            }
        }
    }
}
