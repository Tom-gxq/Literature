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
    public class SysKindRespository : RepositoryBase<SysKindEntity, string>
    {
        public SysKindRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<SysKindEntity> GetDiscountList(int kind, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SysKindEntity>().Where(x => x.Kind == kind);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public bool  AddSysKind(SysKindEntity sysKind)
        {
            var result = this.Insert(sysKind);
            return result > 0;
        }
        public long GetDiscountListCount(int kind)
        {
            return this.Count(x => x.Kind == kind);
        }

        public SysKindEntity GetAssociatorDetail(string kindId)
        {
            return this.Single(x=>x.Id == kindId);
        }
        public bool EditSysKind(SysKindEntity sysKind)
        {
            var result = this.UpdateNonDefaults(sysKind,x=>x.Id == sysKind.Id);
            return result > 0;
        }
        public bool DeleteSysKind(string kindId)
        {
            var result = this.Delete(x=>x.Id == kindId);
            return result > 0;
        }
        public List<SysKindEntity> GetSysKindList(int resEventId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ResEventEntity>();
                q = q.Join<ResEventEntity, SysKindEntity>((a,e)=>a.Id == resEventId && a.Kind == e.Kind);
                return db.Select<SysKindEntity>(q);
            }
        }
    }
}
