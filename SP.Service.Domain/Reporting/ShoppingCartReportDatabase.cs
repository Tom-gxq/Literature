using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class ShoppingCartReportDatabase : IReportDatabase
    {
        private readonly ShoppingCartRespository _repository;
        private readonly ProductRepository _productRepository;
        public ShoppingCartReportDatabase(ShoppingCartRespository repository, ProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public void Add(ShoppingCartsEntity item)
        {
            _repository.AddShoppingCart(item);
        }

        public List<ShoppingCartsDomain> GetMyShoppingCartList(string accountId)
        {
            List<ShoppingCartsDomain> domainList = new List<ShoppingCartsDomain>();
            var list = _repository.GetMyShoppingCartList(accountId);
            foreach(var item in list)
            {
                domainList.Add(ConvertOrderEntityToDomain(item));
            }
            return domainList;
        }
        public List<ShoppingCartsDomain> GetMyShoppingCartListByOrderId(string accountId,string orderId)
        {
            List<ShoppingCartsDomain> domainList = new List<ShoppingCartsDomain>();
            var list = _repository.GetMyShoppingCartListByOrderId(accountId, orderId);
            foreach (var item in list)
            {
                domainList.Add(ConvertOrderEntityToDomain(item));
            }
            return domainList;
        }

        public long GetMyShoppingCartCount(string accountId)
        {
            return _repository.GetMyShoppingCartCount(accountId);
        }

        public bool UpdateShoppingCartQuantity(string cartId, int quantity)
        {
            var retCount = _repository.UpdateShoppingCart(new ShoppingCartsEntity()
            {
                 CartId = cartId,
                 Quantity = quantity,
                 UpdateTime = DateTime.Now
            });
            return retCount > 0;
        }

        public bool UpdateShoppingCartEnabled(string accountId)
        {
            var retCount = _repository.UpdateShoppingCartEnabled(accountId);
            return retCount > 0;
        }

        public bool UpdateShoppingCartOrderId(string orderId, List<string> list)
        {
            var retCount = _repository.UpdateShoppingCartOrderId(orderId, list);
            return retCount > 0;
        }

        public bool RemoveShoppingCart(string cartId)
        {
            var retCount = _repository.RemoveShoppingCart(cartId);
            return retCount > 0;
        }
        public ShoppingCartsDomain GetShoppingCart(string accountId, int shopId,string productId)
        {
            var entity = _repository.GetShoppingCart(accountId, shopId, productId);
            
            return ConvertOrderEntityToDomain(entity);
        }

        public ShoppingCartsDomain GetShoppingCartByOrderIdandProductId(string orderId, string productId)
        {
            var entity = _repository.GetShoppingCartByOrderIdandProductId(orderId, productId);

            return ConvertOrderEntityToDomain(entity);
        }

        private ShoppingCartsDomain ConvertOrderEntityToDomain(ShoppingCartsEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            var shopCart = new ShoppingCartsDomain();
            shopCart.SetMemento(entity);
            if (shopCart.Product != null && !string.IsNullOrEmpty(shopCart.Product.ProductId))
            {
                shopCart.Product = _productRepository.GetProductById(shopCart.Product.ProductId);
            }
            shopCart.CalculateAmount();
            return shopCart;
        }
        
    }
}
