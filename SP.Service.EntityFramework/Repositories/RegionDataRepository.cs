using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class RegionDataRepository : EfRepository<RegionDataEntity>
    {
        public RegionDataRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public List<RegionDataEntity> GetRegionDataList(int dataType)
        {
            return this.Select(x => x.Status == 1 && x.DataType == dataType);
        }

        public RegionEntity GetRegionData(int dataId)
        {
            var result = new RegionEntity();
            var region = this.Single(x => x.DataID == dataId);
            if(region != null)
            {
                result.DataID = region.DataID;
                result.DataName = region.DataName;
                result.ParentDataID = region.ParentDataID.Value;
                var city = this.Single(x => x.DataID == result.ParentDataID);
                if(city != null )
                {
                    result.CityID = city.ParentDataID.Value;
                    result.CityName = city.DataName;
                    var province = this.Single(x => x.DataID == result.CityID);
                    if(province != null)
                    {
                        result.ProvinceID = province.ParentDataID.Value;
                    }
                }
            }
            return result;
        }

        public List<RegionDataEntity> GetChildRegionData(int dataId)
        {
            return this.Select(x => x.Status == 1 && x.ParentDataID == dataId);
        }

        public List<RegionDataEntity>  GetSelectedRegionDataList(string accountId)
        {
            var results = new List<RegionDataEntity>();
            using (var db = OpenDbConnection())
            {
                var q = db.From<RegionDataEntity>();
                q = q.Join<RegionDataEntity, AccountAddressEntity>((a, e) => e.AccountId == accountId && a.DataID == e.RegionID && e.RegionID > 1);
                return db.Select<RegionDataEntity>(q);
            }
        }
    }
}
