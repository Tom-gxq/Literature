using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Events
{
    [Serializable]
    public class Event : IEvent
    {
        public int Version { get; set; }
        public Guid AggregateId { get; set; }
        public string CommandId { get; set; }
    }
}
