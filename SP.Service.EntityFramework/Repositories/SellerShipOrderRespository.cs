using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SellerShipOrderRespository : EfRepository<ShipOrderEntity>
    {
        public SellerShipOrderRespository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool Add(ShipOrderEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }
        public bool UpdateShipOrderStatus(ShipOrderEntity item)
        {
            var result = this.UpdateNonDefaults(item, x => x.OrderId == item.OrderId && x.ProductId == item.ProductId
            && x.ShipId == item.ShipId && x.ShipTo == item.ShipTo);
            return result > 0;
        }
        public List<ShipOrderEntity> GetShippingOrdersByOrderId(string orderId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ShipOrderEntity>();
                q = q.Where(x => x.OrderId == orderId && (x.IsShipped == false || x.IsShipped == null));
                return db.Select(q);
            }
        }
        public ShipOrderEntity GetShippingOrdersById(int shipOrderId)
        {
            return this.Single(x=>x.Id == shipOrderId);
        }
    }
}
