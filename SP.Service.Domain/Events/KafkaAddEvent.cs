using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.Sender;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class KafkaAddEvent: Event
    {
        public AbstractEntity Prducer { get; internal set; }

        public KafkaAddEvent(AbstractEntity prducer)
        {
            Prducer = prducer;
        }
    }
}
