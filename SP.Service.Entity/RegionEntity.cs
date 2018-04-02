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
        public int CityID { get; set; }
        [Ignore]
        public string CityName { get; set; }
        public int ProvinceID { get; set; }
    }
}
