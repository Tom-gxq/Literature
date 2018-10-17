using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ShoppingCartRespository:EfRepository<ShoppingCartsEntity>
    {
        public ShoppingCartRespository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public ShoppingCartsEntity GetShoppingCartsById(string cartId)
        {
            var result = this.Single(x => x.CartId == cartId);
            return result;
        }

        public List<ShoppingCartsEntity> GetShoppingCartsByOrderId(string orderId)
        {
            var result = this.Select(x => x.OrderId == orderId );
            return result;
        }

        public int UpdateShoppingCart(ShoppingCartsEntity entity)
        {
            return this.UpdateNonDefaults(entity, x => x.CartId == entity.CartId);
        }
        public int UpdateShoppingCartEnabled(string accountId)
        {
            return this.UpdateNonDefaults(new ShoppingCartsEntity()
            {
                IsEnabled = true
            }, x => x.AccountId == accountId && x.IsEnabled == false);
        }

        public int UpdateShoppingCartOrderId(string orderId, List<string> list)
        {
            return this.UpdateNonDefaults(new ShoppingCartsEntity()
            {
                OrderId = orderId
            }, x => list.Contains(x.CartId));
        }

        public long AddShoppingCart(ShoppingCartsEntity entity)
        {
            return this.Insert(entity);
        }

        public List<ShoppingCartsEntity> GetMyShoppingCartList(string accountId)
        {
            return this.Select(x=>x.AccountId == accountId&& x.IsEnabled == false && x.Quantity > 0 
            && x.OrderId == null && x.UpdateTime >= DateTime.Now.AddMinutes(-10));
        }
        public List<ShoppingCartsEntity> GetMyShoppingCartListByOrderId(string accountId,string orderId)
        {
            return this.Select(x => x.AccountId == accountId  && x.Quantity > 0 && x.OrderId == orderId );
        }

        public long GetMyShoppingCartCount(string accountId)
        {
            return this.Count(x => x.AccountId == accountId && x.IsEnabled == false && x.Quantity > 0 && x.OrderId == null);
        }
        public long RemoveShoppingCart(string cartId)
        {
            return this.UpdateNonDefaults(new ShoppingCartsEntity()
            {
                IsEnabled = true
            }, x => x.CartId == cartId );
        }
        public ShoppingCartsEntity GetShoppingCart(string accountId, int shopId, string productId)
        {
            return this.Single(x => x.AccountId == accountId &&x.ShopId== shopId && x.ProductId == productId
            && x.IsEnabled == false && x.OrderId == null && x.CreateTime >=DateTime.Parse(DateTime.Now.ToShortDateString()));
        }

        public ShoppingCartsEntity GetShoppingCartByOrderIdandProductId(string orderId, string productId)
        {
            return this.Single(x => x.OrderId == orderId && x.ProductId == productId);
        }
    }
}
