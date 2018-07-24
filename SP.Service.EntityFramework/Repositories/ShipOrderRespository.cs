using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ShipOrderRespository : EfRepository<ShippingOrdersEntity>
    {
        public ShipOrderRespository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool Add(ShippingOrdersEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }

        public List<ShippingOrdersEntity> GetShippingOrdersByOrderId(string orderId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ShippingOrdersEntity>();
                q = q.Where(x=>x.OrderId == orderId && x.IsShipped != true);
                return db.Select(q);
            }
        }

        public ShippingOrdersEntity GetShippingOrders(string orderId,string accountId,string productId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ShippingOrdersEntity>();
                q = q.Where(x => x.OrderId == orderId && x.IsShipped != true && x.ShippingId == accountId && x.ProductId == productId);
                return db.Single(q);
            }
        }
    }
}
