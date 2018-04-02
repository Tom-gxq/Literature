using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_EventRelation")]
    public class EventRelationEntity : Entity
    {
        [AutoIncrement]
        [Alias("Id")]
        public override int Id { get; set; }
        public int? SysEventId { get; set; }
        public int? ResEventId { get; set; }
        public int? Quantity { get; set; }
        public string KindId { get; set; }
    }
}
