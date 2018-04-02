using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_SysKind")]
    public class SysKindEntity : Entity<string>
    {
        //[AutoIncrement]
        //[Alias("Id")]
        //public string Id { get; set; }
        [Alias("KindId")]
        public override string Id { get; set; }
        public int? Kind { get; set; }
        public int? Quantity { get; set; }
        public int? Unit { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public double? DiscountValue { get; set; }
    }
}
