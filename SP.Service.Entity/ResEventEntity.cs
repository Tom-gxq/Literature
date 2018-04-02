using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ResEvent")]
    public class ResEventEntity : BaseEntity
    {
        [AutoIncrement]
        [Alias("Id")]
        public int? ID { get; set; }
        public string EventName { get; set; }
        public int? Kind { get; set; }
    }
}
