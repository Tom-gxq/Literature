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
    public class ResEventRespository : RepositoryBase<ResEventEntity, int>
    {
        public ResEventRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<ResEventEntity> GetResEventList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ResEventEntity>();
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public long GetResEventListCount()
        {
            return this.Count();
        }
        public bool AddResEvent(ResEventEntity resEvent)
        {
            var result = this.Insert(resEvent);
            return result > 0;
        }
        public bool DeleteResEvent(int id)
        {
            var result = this.Delete(x=>x.Id == id);
            return result > 0;
        }
        public List<ResEventEntity> GetResEventList()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ResEventEntity>();
                return db.Select(q);
            }
        }
        public List<SysEventEntity> GetSysEventList()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SysEventEntity>().Where(x=>x.EventType == 0);
                return db.Select(q);
            }
        }
    }
}
