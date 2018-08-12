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
    public class SupplersRepository : RepositoryBase<SuppliersEntity, int>
    {
        public SupplersRepository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public SuppliersEntity GetSupplerById(int suppliersId)
        {
            var result = this.Single(x => x.Id == suppliersId);
            return result;
        }
        public bool UpdateSupplerStatus(SuppliersEntity entity)
        {
            var result = this.UpdateNonDefaults(entity, x => x.Id == entity.Id);
            return result > 0;
        }
        public bool UpdateSuppler(SuppliersEntity entity)
        {
            var result = this.UpdateNonDefaults(entity, x => x.Id == entity.Id);
            return result > 0;
        }

        public List<SuppliersEntity> GeAllSupplerList()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersEntity>().Where(x => x.Status == 0)
                    .OrderByDescending(x => x.CreateTime);
                return db.Select(q);
            }
        }

        public List<SuppliersEntity> SearchSuppler(string productId, int supplerId, int type, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersEntity>();
                if(!string.IsNullOrEmpty(productId))
                {
                    q = q.Join<SuppliersEntity, ProductEntity>((a,b)=>a.AccountId == b.SuppliersId && b.ProductId == productId);
                }
                if (supplerId > 0)
                {
                    if (type >= 0)
                    {
                        q = q.Where(x => x.Id == supplerId && x.Type == type);
                    }
                    else
                    {
                        q = q.Where(x => x.Id == supplerId);
                    }
                }
                else if(type >= 0)
                {
                    q = q.Where(x => x.Type == type && x.Status == 0);
                }
                else
                {
                    q = q.Where(x => x.Status == 0);
                }
                q = q.OrderByDescending(x => x.CreateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public int SearchSupplerCount(string productId, int supplerId, int type)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersEntity>();
                if (!string.IsNullOrEmpty(productId))
                {
                    q = q.Join<SuppliersEntity, ProductEntity>((a, b) => a.AccountId == b.SuppliersId && b.ProductId == productId);
                }
                if (supplerId > 0)
                {
                    if (type >= 0)
                    {
                        q = q.Where(x => x.Id == supplerId && x.Type == type);
                    }
                    else
                    {
                        q = q.Where(x => x.Id == supplerId);
                    }
                }
                else if (type >= 0)
                {
                    q = q.Where(x => x.Type == type && x.Status == 0);
                }
                else
                {
                    q = q.Where(x => x.Status == 0);
                }
                return db.Select(q).Count();
            }
        }

        public List<SuppliersEntity> SearchSellerData(string name)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersEntity>().Where(x => x.SuppliersName.Contains(name))
                    .OrderByDescending(x => x.CreateTime);
                return db.Select(q);
            }
        }

        public SuppliersEntity GetSellerDataByAccountId(string accountId)
        {
            return this.Single(x=>x.AccountId == accountId);
        }
    }
}
