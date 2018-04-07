using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_Shop")]
    public class ShopEntity : Entity
    {
        [AutoIncrement]
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

        public string ShopName { get; set; }
        public int? DisplaySequence { get; set; }
        public int? RegionId { get; set; }
        public string OwnerId { get; set; }
        public string MetaKeywords { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? ShopType { get; set; }
    }
}
