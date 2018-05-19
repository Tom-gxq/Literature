using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class OrderRepository : EfRepository<OrdersEntity>
    {
        public OrderRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddOrder(OrdersEntity order)
        {
            var result = this.Insert(order);
            return result > 0;
        }
        public bool UpdateOrderStatus(OrdersEntity order)
        {
            var result = this.UpdateNonDefaults(order,x=>x.OrderId == order.OrderId);
            return result > 0;
        }

        public List<OrdersEntity> GetMyHistoryOrderList(string accountId, DateTime orderDate)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<OrdersEntity>();
                q = q.Join<OrdersEntity, ShoppingCartsEntity>((e, a) => a.OrderId == e.OrderId
                && e.OrderDate >= orderDate && (e.OrderStatus == 2 || e.OrderStatus == 5) && e.AccountId == accountId);
                q = q.Join<ShoppingCartsEntity, ProductEntity>((e, a) => a.ProductId == e.ProductId);
                return db.Select(q);
            }
        }
        public List<DateTime> GetMyMaxHistoryOrder(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<OrdersEntity>().Where(e=> (e.OrderStatus == 2 || e.OrderStatus == 5) && e.AccountId == accountId);
                q = q.Select(x => Sql.Max(x.OrderDate));
                return db.Select<DateTime>(q);
            }
        }
        public List<OrdersEntity> GetMyOrderList(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<OrdersEntity>();
                q = q.Join<OrdersEntity, ShoppingCartsEntity>((e, a) => a.OrderId == e.OrderId && e.OrderStatus == 2 && e.AccountId== accountId);
                q = q.Join<ShoppingCartsEntity, ProductEntity>((e, a) => a.ProductId == e.ProductId);
                return db.Select(q);
            }
        }

        public List<OrdersEntity> SearchOrderKeywordList(string accountId,string keyWord)
        {
            var result = this.Select(x => x.AccountId == accountId && x.Meta_Keywords.Contains(keyWord));
            return result;
        }
        public OrdersEntity GetOrderByOrderId(string orderId)
        {
            var result = this.Single(x => x.OrderId == orderId);
            return result;
        }
        public OrdersEntity GetOrderByOrderCode(string orderCode)
        {
            var result = this.Single(x => x.OrderCode == orderCode);
            return result;
        }

        public string GetOrderCode(string orderCodeBefore)
        {
            return string.Empty;
        }

        public List<OrdersEntity> GetSchoolLeadList(string accountId,int orderStatus)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<OrdersEntity>();

                q = q.Join<OrdersEntity, ShoppingCartsEntity>((e, a) => a.OrderId == e.OrderId && e.OrderStatus == orderStatus && e.OrderDate >= DateTime.Parse(DateTime.Now.ToShortDateString()));
                q = q.Join<ShoppingCartsEntity, ShopEntity>((e, a) => a.Id == e.ShopId && a.OwnerId == accountId);
                if (orderStatus == 2)
                {
                    q = q.OrderBy(x => x.OrderCode);
                }
                else
                {
                    q = q.OrderByDescending(x => x.OrderCode);
                }
                return db.Select(q);
            }
        }
    }
}
