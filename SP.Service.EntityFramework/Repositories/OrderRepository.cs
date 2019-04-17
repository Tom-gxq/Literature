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
                q = q.Join<OrdersEntity, ShoppingCartsEntity>((e, a) => a.OrderId == e.OrderId && e.OrderType==0
                && e.OrderDate >= orderDate && (e.OrderStatus == 2 || e.OrderStatus == 5) && e.AccountId == accountId);
                q = q.Join<ShoppingCartsEntity, ProductEntity>((e, a) => a.ProductId == e.ProductId);
                q = q.OrderByDescending(x=>x.OrderDate);
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

        public List<OrdersEntity> GetSchoolLeadList(string accountId,int orderStatus,int orderType)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<OrdersEntity>();

                if (orderType == 1)
                {
                    q = q.Join<OrdersEntity, ShoppingCartsEntity>((e, a) => a.OrderId == e.OrderId 
                    && e.OrderStatus == orderStatus && e.OrderType == orderType );
                    if (orderStatus == 2)
                    {
                        q = q.Join<ShoppingCartsEntity, ShipOrderEntity>((e, a) => a.OrderId == e.OrderId
                        && e.ShopId == a.ShopId && e.ProductId == a.ProductId && e.AccountId == a.ShipTo
                        && a.ShipId == accountId && a.IsShipped == false);
                    }
                }
                else
                {
                    q = q.Join<OrdersEntity, ShoppingCartsEntity>((e, a) => a.OrderId == e.OrderId && e.OrderStatus == orderStatus
                    && e.OrderType == orderType && e.OrderDate >= DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString()));
                }
                q = q.Join<ShoppingCartsEntity, ShippingOrdersEntity>((e, a) => a.ShopId == e.ShopId && a.ShippingId == accountId && e.OrderId == a.OrderId );
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

        public List<ShippingOrderFullEntity> GetShipOrderList(string accountId, int orderStatus, int orderType)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ShippingOrdersEntity>();
                q = q.Join<ShippingOrdersEntity, OrdersEntity>((e,a)=>e.OrderId==a.OrderId && e.ShippingId == accountId
                    && e.ShippingDate >= DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString()) && a.OrderStatus == orderStatus);
                q = q.Join<ShippingOrdersEntity, ProductEntity>((e,b)=>e.ProductId == b.ProductId);
                if (orderStatus == 2)
                {
                    q = q.Where(x => (x.IsShipped == false || x.IsShipped == null));
                    q = q.OrderBy(x => x.ShippingDate);
                }
                else
                {
                    q = q.Where(x => x.IsShipped == true);
                    q = q.OrderByDescending(x => x.ShippingDate);
                }
                return db.Select<ShippingOrderFullEntity>(q);
            }            
        }
        public List<ShippingOrderFullEntity> GetPurchaseOrderList(string accountId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<OrdersEntity>();
                q = q.Join<OrdersEntity, ShoppingCartsEntity>((e, a) => e.OrderId == a.OrderId && a.AccountId == accountId && e.OrderType == 1);
                q = q.OrderByDescending(x => x.OrderDate);
                return db.Select<ShippingOrderFullEntity>(q);
            }
        }
    }
}
