using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_SysEvent")]
    public class SysEventEntity : BaseEntity
    {
        [AutoIncrement]
        [Alias("Id")]
        public int? ID { get; set; }
        public string EventName { get; set; }
        public int? EventType { get; set; }
    }
}
