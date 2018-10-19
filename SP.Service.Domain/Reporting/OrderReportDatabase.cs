using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class OrderReportDatabase : IReportDatabase
    {
        private readonly OrderRepository _repository;
        private readonly ShoppingCartRespository _cartRepository;
        public OrderReportDatabase(OrderRepository repository, ShoppingCartRespository cartRepository)
        {
            _repository = repository;
            _cartRepository = cartRepository;
        }

        public void Add(OrdersEntity item)
        {
            _repository.AddOrder(item);
        }
        public bool UpdateOrderStatus(OrdersEntity item)
        {
            var result = _repository.UpdateOrderStatus(item);
            return result;
        }

        public List<OrderDomain> GetMyOrderList(string accountId, DateTime orderDate)
        {
            var orderDomainList = new List<OrderDomain>();
            var orderList = new List<OrdersEntity>();
            orderList = _repository.GetMyHistoryOrderList(accountId, orderDate);

            foreach (var item in orderList)
            {
                var order = ConvertOrderEntityToDomain(item);
                if (order != null)
                {
                    orderDomainList.Add(order);
                }
            }
            return orderDomainList;
        }
        public DateTime GetMyMaxHistoryOrder(string accountId)
        {
            var orderDomainList = new List<OrderDomain>();
            var orderList = new List<DateTime>();
            orderList = _repository.GetMyMaxHistoryOrder(accountId);
            if(orderList != null && orderList.Count > 0)
            {
                return orderList[0];
            }
            else
            {
                return DateTime.MaxValue;
            }
        }

        public List<OrderDomain> SearchOrderKeywordList(string accountId, string keyWord)
        {
            var orderDomainList = new List<OrderDomain>();
            var orderList = _repository.SearchOrderKeywordList(accountId, keyWord);
            foreach (var item in orderList)
            {
                var order = ConvertOrderEntityToDomain(item);
                orderDomainList.Add(order);
            }
            return orderDomainList;
        }
        public OrderDomain GetOrderByOrderId(string orderId)
        {
            var order = _repository.GetOrderByOrderId(orderId);
            
            return ConvertOrderEntityToDomain(order); 
        }
        public OrderDomain GetOrderByOrderId(string orderId,string accountId)
        {
            var order = _repository.GetOrderByOrderId(orderId);

            return ConvertOrderEntityToDomain(order, accountId);
        }
        public LeadOrderDomain GetLeadOrderDomainByOrderId(string orderId)
        {
            var order = _repository.GetOrderByOrderId(orderId);

            return ConvertOrderEntityToLeadOrderDomain(order);
        }
        public LeadOrderDomain GetLeadOrderDomainByOrderCode(string orderCode)
        {
            var order = _repository.GetOrderByOrderCode(orderCode);

            return ConvertOrderEntityToLeadOrderDomain(order);
        }
        public List<LeadOrderDomain> GetSchoolLeadList(string accountId, int orderStatus, int orderType)
        {
            var domainList = new List<LeadOrderDomain>();
            var list = _repository.GetSchoolLeadList(accountId, orderStatus, orderType);
            var result = list.GroupBy(s => s.OrderId);
            foreach (var item in result)
            {
                int index = 0;
                foreach (var orderInfo in item)
                {
                    if(index > 0)
                    {
                        break;
                    }
                    var order = ConvertOrderEntityToLeadOrderDomain(orderInfo, accountId);
                    if (order != null)
                    {
                        domainList.Add(order);
                    }
                    index++;
                }
            }
            return domainList;
        }

        public ShoppingCartsEntity GetShoppingCartsById(string cartId)
        {
            return _cartRepository.GetShoppingCartsById(cartId);
        }
        public List<ShoppingCartsEntity> GetShoppingCartsByOrderId(string orderId)
        {
            return _cartRepository.GetShoppingCartsByOrderId(orderId);
        }

        public bool UpdateShoppingCart(ShoppingCartsEntity entity)
        {
            var result = _cartRepository.UpdateShoppingCart(entity);
            return result > 0;
        }

        public List<LeadOrderDomain> GetShipOrderList(string accountId, int orderStatus, int orderType)
        {
            var domainList = new List<LeadOrderDomain>();
            var list = _repository.GetShipOrderList(accountId, orderStatus, orderType);
            var result = list.GroupBy(s => s.OrderId);
            foreach (var item in result)
            {
                var domain = new LeadOrderDomain();
                var shoppingCartList = new List<ShoppingCartsDomain>();
                int index = 0;
                foreach (var orderInfo in item)
                {
                    var order = ConvertOrderEntityToLeadOrderDomain(orderInfo);
                    if (order != null)
                    {
                        if (index == 0)
                        {
                            domain.OrderId = order.OrderId;
                            domain.OrderAddress = order.OrderAddress;
                            domain.IsVip = order.IsVip;
                            var accountReportDatabase = IocManager.Instance.Resolve(typeof(AccountReportDatabase)) as AccountReportDatabase;
                            var account = accountReportDatabase.GetAccountById(order.ShipTo);
                            if (account != null)
                            {
                                domain.SetAccountMemento(account.GetMemento());
                                var accountInfoReportDatabase = IocManager.Instance.Resolve(typeof(AccountInfoReportDatabase)) as AccountInfoReportDatabase;
                                var accountInfo = accountInfoReportDatabase.GetAccountInfoById(order.ShipTo);
                                domain.SetAccountInfoMemento(accountInfo.GetMemento());
                            }
                            else
                            {
                                System.Console.WriteLine($"GetShipOrderList order.ShipTo  is not Exsit [{order?.ShipTo??string.Empty}]");
                            }                            
                            domain.PayDate = order.PayDate;
                            domain.OrderDate = order.OrderDate;
                        }
                        var cart = new ShoppingCartsDomain();
                        cart.OrderId = order.OrderId;
                        cart.Product = new ProductEntity();
                        cart.Product.ProductName = order.ProductName;
                        cart.Product.ProductId = order.ProductId;
                        cart.Quantity = order.Stock;
                        cart.ShipOrderId = order.ShipOrderId;
                        shoppingCartList.Add(cart);
                        index++;
                    }
                }
                domain.SetMemenShoppingCartto(shoppingCartList);
                domainList.Add(domain);
            }
            return domainList;
        }

        private OrderDomain ConvertOrderEntityToDomain(OrdersEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            var order = new OrderDomain();
            order.SetMemento(entity);
            var orderCartList = _cartRepository.GetShoppingCartsByOrderId(entity.OrderId);
            var productList = new List<ProductDomain>();
            foreach (var cart in orderCartList)
            {                
                var productReportDatabase = IocManager.Instance.Resolve(typeof(ProductReportDatabase)) as ProductReportDatabase; ;
                var product = productReportDatabase.GetProductDomainById(cart.ProductId);
                
                product.Quantity = cart.Quantity ?? 0;                
                productList.Add(product);
            }
            order.SetMemenProductto(productList);
            return order;
        }
        private OrderDomain ConvertOrderEntityToDomain(OrdersEntity entity,string accountId)
        {
            if (entity == null)
            {
                return null;
            }
            var order = new OrderDomain();
            order.SetMemento(entity);
            var orderCartList = _cartRepository.GetShoppingCartsByOrderId(entity.OrderId);
            var productList = new List<ProductDomain>();
            foreach (var cart in orderCartList)
            {
                var productReportDatabase = IocManager.Instance.Resolve(typeof(ProductReportDatabase)) as ProductReportDatabase; ;
                var product = productReportDatabase.GetSellerProduct(cart.ProductId, accountId);
                if (product != null)
                {
                    product.Quantity = cart.Quantity ?? 0;                    
                    productList.Add(product);
                }
            }
            order.SetMemenProductto(productList);
            return order;
        }
        private ShipOrderDomain ConvertOrderEntityToLeadOrderDomain(ShippingOrderFullEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var order = new ShipOrderDomain();
            order.SetMemento(entity);

            
            return order;
        }
        private LeadOrderDomain ConvertOrderEntityToLeadOrderDomain(OrdersEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            var order = new LeadOrderDomain();
            order.SetMemento(entity);
            var orderCartList = _cartRepository.GetShoppingCartsByOrderId(entity.OrderId);
            var shoppingCartList = new List<ShoppingCartsDomain>();
            foreach (var cart in orderCartList)
            {
                var shoppingCart = new ShoppingCartsDomain();
                shoppingCart.SetMemento(cart);
                var productReportDatabase = IocManager.Instance.Resolve(typeof(ProductReportDatabase)) as ProductReportDatabase;
                var product = productReportDatabase.GetProductDomainById(cart.ProductId);
                shoppingCart.SetMemenProductto(product.GetMemento());
                
                shoppingCart.CalculateAmount();
                shoppingCartList.Add(shoppingCart);
            }
            order.SetMemenShoppingCartto(shoppingCartList);
            if(!string.IsNullOrEmpty(entity.AccountId))
            {
                var accountReportDatabase = IocManager.Instance.Resolve(typeof(AccountReportDatabase)) as AccountReportDatabase;
                var account = accountReportDatabase.GetAccountById(entity.AccountId);
                order.SetAccountMemento(account.GetMemento());
                var accountInfoReportDatabase = IocManager.Instance.Resolve(typeof(AccountInfoReportDatabase)) as AccountInfoReportDatabase;
                var accountInfo = accountInfoReportDatabase.GetAccountInfoById(entity.AccountId);
                order.SetAccountInfoMemento(accountInfo.GetMemento());
                if (entity.AddressId != null && entity.AddressId.Value > 0)
                {
                    var addressReportDatabase = IocManager.Instance.Resolve(typeof(AddressReportDatabase)) as AddressReportDatabase;
                    var address = addressReportDatabase.GetAddressById(entity.AddressId.Value, entity.AccountId);
                    order.SetAddressMemento(address.GetMemento());
                }
            }
            if(shoppingCartList.Count > 0)
            {
                var shopReportDatabase = IocManager.Instance.Resolve(typeof(ShopReportDatabase)) as ShopReportDatabase;
                var shop = shopReportDatabase.GetShopById(shoppingCartList[0].ShopId);
                order.SetShopMemento(shop.GetMemento());
            }
            
            return order;
        }

        private LeadOrderDomain ConvertOrderEntityToLeadOrderDomain(OrdersEntity entity, string accountId)
        {
            if (entity == null)
            {
                return null;
            }
            var order = new LeadOrderDomain();
            order.SetMemento(entity);
            var orderCartList = _cartRepository.GetShoppingCartsByOrderId(entity.OrderId);
            var shoppingCartList = new List<ShoppingCartsDomain>();
            foreach (var cart in orderCartList)
            {
                var shoppingCart = new ShoppingCartsDomain();
                shoppingCart.SetMemento(cart);
                var productReportDatabase = IocManager.Instance.Resolve(typeof(ProductReportDatabase)) as ProductReportDatabase;
                var product = productReportDatabase.GetSellerProduct(cart.ProductId, accountId);
                if (product != null)
                {
                    shoppingCart.SetMemenProductto(product.GetMemento());
                    var shipReportDatabase = IocManager.Instance.Resolve(typeof(ShipOrderReportDatabase)) as ShipOrderReportDatabase;
                    
                    shoppingCart.CalculateAmount();
                    shoppingCartList.Add(shoppingCart);
                }
            }
            order.SetMemenShoppingCartto(shoppingCartList);
            if (!string.IsNullOrEmpty(entity.AccountId))
            {
                var accountReportDatabase = IocManager.Instance.Resolve(typeof(AccountReportDatabase)) as AccountReportDatabase;
                var account = accountReportDatabase.GetAccountById(entity.AccountId);
                order.SetAccountMemento(account.GetMemento());
                var accountInfoReportDatabase = IocManager.Instance.Resolve(typeof(AccountInfoReportDatabase)) as AccountInfoReportDatabase;
                var accountInfo = accountInfoReportDatabase.GetAccountInfoById(entity.AccountId);
                order.SetAccountInfoMemento(accountInfo.GetMemento());
                if (entity.AddressId != null && entity.AddressId.Value > 0)
                {
                    var addressReportDatabase = IocManager.Instance.Resolve(typeof(AddressReportDatabase)) as AddressReportDatabase;
                    var address = addressReportDatabase.GetAddressById(entity.AddressId.Value, entity.AccountId);
                    order.SetAddressMemento(address.GetMemento());
                }
            }
            if (shoppingCartList.Count > 0)
            {
                var shopReportDatabase = IocManager.Instance.Resolve(typeof(ShopReportDatabase)) as ShopReportDatabase;
                var shop = shopReportDatabase.GetShopById(shoppingCartList[0].ShopId);
                order.SetShopMemento(shop.GetMemento());
            }

            return order;
        }
    }
}
