using Lib.EntityFramework.EntityFramework;
using ServiceStack.OrmLite;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework.Repositories
{
    public class ShipOrderRespository : RepositoryBase<ShippingOrdersEntity,int>
    {
        public ShipOrderRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public bool Add(ShippingOrdersEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }
        public bool UpdateShippingOrderStatus(ShippingOrdersEntity item)
        {
            var result = this.UpdateNonDefaults(item, x => x.Id == item.Id);
            return result > 0;
        }

        public List<ShippingOrdersEntity> GetShippingOrdersByOrderId(string orderId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShippingOrdersEntity>();
                q = q.Where(x => x.OrderId == orderId && (x.IsShipped == false || x.IsShipped == null));
                return db.Select(q);
            }
        }

        public ShippingOrdersEntity GetShippingOrders(string orderId, string accountId, string productId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShippingOrdersEntity>();
                q = q.Where(x => x.OrderId == orderId && (x.IsShipped != true || x.IsShipped == null) && x.ShippingId == accountId && x.ProductId == productId);
                return db.Single(q);
            }
        }
        public ShippingOrdersEntity GetShippingOrders(string orderId, string accountId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShippingOrdersEntity>();
                q = q.Where(x => x.OrderId == orderId && (x.IsShipped != true || x.IsShipped == null) && x.ShippingId == accountId);
                return db.Single(q);
            }
        }

        public ShippingOrdersEntity GetShippingOrdersById(int shipOrderId)
        {
            return this.Single(x => x.Id == shipOrderId);
        }

        public List<ShippingOrdersEntity> GetOrderShippingByOrderId(string orderId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShippingOrdersEntity>();
                q = q.Where(x => x.OrderId == orderId );
                return db.Select(q);
            }
        }
    }
}
