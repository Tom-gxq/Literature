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
    public class RegionRespository: RepositoryBase<RegionEntity, int>
    {
        public RegionRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public bool AddRegionData(RegionEntity data)
        {
            var result = this.Insert(data);
            return result > 0;
        }
        public bool DelRegionData(int dataId)
        {
            var result = this.Delete(x=>x.Id == dataId);
            return result > 0;
        }
        public List<RegionEntity> GetRegionData(int dataType)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionEntity>().Where(x=>x.DataType == dataType);
                return db.Select(q);
            }
        }
        public RegionEntity GetRegionData(int parentId, string dataName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionEntity>().Where(x => x.ParentDataID == parentId && x.DataName == dataName);
                return db.Single(q);
            }
        }


        public List<RegionEntity> SearchRegionData(string dataName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionEntity>().Where(x => x.DataName.Contains(dataName));
                return db.Select(q);
            }
        }

        public List<RegionEntity> GetChildRegionData(int parentId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionEntity>().Where(x => x.ParentDataID == parentId);
                return db.Select(q);
            }
        }

        public List<RegionEntity> GetChildRegionData(int parentId, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionEntity>().Where(x => x.ParentDataID == parentId);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public long GetChildRegionDataCount(int parentId)
        {
            return this.Count(x=>x.ParentDataID == parentId);
        }
        public RegionEntity GetRegionDataDetail(int dataId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionEntity>().Where(x => x.Id == dataId);
                return db.Single(q);
            }
        }
        public bool EditRegionData(RegionEntity region)
        {
            var result= this.UpdateNonDefaults(region, x => x.Id == region.Id);
            return result > 0;
        }
    }
}
