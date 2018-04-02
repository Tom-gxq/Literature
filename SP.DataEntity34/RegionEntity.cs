using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_RegionData")]
    public class RegionEntity : Entity<int>
    {
        [AutoIncrement]
        [Alias("DataID")]
        public override int Id
        {
            get
            {
                return base.Id;
            }

            set
            {
                base.Id = value;
            }
        }
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
    }
}
