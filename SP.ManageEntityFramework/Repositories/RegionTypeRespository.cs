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
    public class RegionTypeRespository: RepositoryBase<RegionTypeEntity, int>
    {
        public RegionTypeRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<RegionTypeFullEntity> GetRegionTypeList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionTypeEntity>();
                q = q.Join<RegionTypeEntity, RegionEntity>((a,e)=>a.RegionId == e.Id);
                q = q.Join<RegionTypeEntity, ProductTypeEntity>((a, e) => a.TypeId == e.Id);
                q = q.OrderBy(x=>x.RegionId).OrderBy(x => x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select<RegionTypeFullEntity>(q);
            }
        }
        public int GetRegionTypeCount()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionTypeEntity>();
                return db.Select(q).Count();
            }
        }

        public bool AddRegionType(RegionTypeEntity data)
        {
            var result = this.Insert(data);
            return result > 0;
        }

        public bool DelRegionType(int id)
        {
            var result = this.Delete(x => x.Id == id);
            return result > 0;
        }

        public List<RegionTypeFullEntity> SearchRegionTypeByKeyWord(string keywords)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionTypeEntity>();
                q = q.Join<RegionTypeEntity, RegionEntity>((a, e) => a.RegionId == e.Id && e.DataName.Contains(keywords));
                q = q.Join<RegionTypeEntity, ProductTypeEntity>((a, e) => a.TypeId == e.Id);
                q = q.OrderBy(x => x.RegionId).OrderBy(x => x.DisplaySequence);                
                return db.Select<RegionTypeFullEntity>(q);
            }
        }

        
    }
}
