using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_RegionData")]
    public class RegionDataEntity : BaseEntity
    {
        [AutoIncrement]
        public int DataID { get; set; }
        /**
        *  区域名称
        */
        public string DataName { get; set; }
        /**
        *  区域父ID
        */
        public int? ParentDataID { get; set; }

        public int? DataType { get; set; }

        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? DisplaySequence { get; set; }
    }
}
