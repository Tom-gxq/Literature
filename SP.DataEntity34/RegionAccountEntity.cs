using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_RegionAccount")]
    public class RegionAccountEntity : Entity<string>
    {
        [Alias("AccountId")]
        public override string Id { get; set; }
        public int? RegionId { get; set; }
        public int? Status { get; set; }
    }
}
