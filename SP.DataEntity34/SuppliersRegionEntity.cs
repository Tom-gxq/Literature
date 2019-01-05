using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_SuppliersRegion")]
    public class SuppliersRegionEntity : Entity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public int? SuppliersId { get; set; }
        public int? RegionID { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
