using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    public class EventFullInfoEntity : BaseEntity
    {
        public int SysEventId { get; set; }
        [Alias("SP_SysEvent.EventName")]
        public string SysEventName { get; set; }
        public int ResEventId { get; set; }
        [Alias("SP_ResEvent.EventName")]
        public string ResEventName { get; set; }
        public int Quantity { get; set; }
        public string KindId { get; set; }
        public string Description { get; set; }
        public int Unit { get; set; }
    }
}
