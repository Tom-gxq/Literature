using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    public class RegionEntity : BaseEntity
    {
        public int DataID { get; set; }
        /**
        *  区域名称
        */
        public string DataName { get; set; }
        /**
        *  区域父ID
        */
        public int ParentDataID { get; set; }
        [Ignore]
        public int BuiddingID { get; set; }
        [Ignore]
        public string BuiddingName { get; set; }
        [Ignore]
        public int DistrictID { get; set; }
        [Ignore]
        public string DistrictName { get; set; }
        [Ignore]
        public int SchoolID { get; set; }
        [Ignore]
        public string SchoolName { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
