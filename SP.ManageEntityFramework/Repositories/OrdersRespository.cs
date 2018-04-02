using Lib.EntityFramework.EntityFramework;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace SP.ManageEntityFramework.Repositories
{
    public class OrdersRespository: RepositoryBase<OrdersEntity,int>
    {

        public OrdersRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<OrdersEntity> GetOrderList(int status, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<OrdersEntity>().Where(x => x.OrderStatus == status).OrderByDescending(x => x.OrderDate);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public long GetOrderListCount(int status)
        {
            return this.Count(x => x.OrderStatus == status);
        }
        public List<OrdersEntity> SearchOrderListByKeyWord(string keyWord, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<OrdersEntity>().Where(x => x.Meta_Keywords.Contains(keyWord)).OrderByDescending(x => x.UpdateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
    }
}
