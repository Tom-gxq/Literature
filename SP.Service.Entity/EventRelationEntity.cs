using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_EventRelation")]
    public class EventRelationEntity : BaseEntity
    {
        [AutoIncrement]
        [Alias("Id")]
        public int? ID { get; set; }
        public int? SysEventId { get; set; }
        public int? ResEventId { get; set; }
        public int? Quantity { get; set; }
        public string KindId { get; set; }
    }
}
