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
                var building = this.Single(x => x.DataID == result.ParentDataID);
                if(building != null )
                {
                    result.BuiddingID = building.DataID;
                    result.BuiddingName = building.DataName;
                    var district = this.Single(x => x.DataID == building.ParentDataID.Value);
                    if (district != null)
                    {
                        result.DistrictID = district.DataID;
                        result.DistrictName = district.DataName;
                        var school = this.Single(x => x.DataID == district.ParentDataID.Value);
                        if (school != null)
                        {
                            result.SchoolID = school.DataID;
                            result.SchoolName = school.DataName;
                        }
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
