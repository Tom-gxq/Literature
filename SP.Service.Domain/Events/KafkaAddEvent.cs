using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.Sender;
using SP.Service.Domain.Util;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class KafkaAddEvent: Event
    {
        public AbstractEntity Prducer { get; set; }

        public KafkaAddEvent(AbstractEntity prducer) : base(KafkaConfig.EventBusTopicTitle)
        {
            Prducer = prducer;
            this.EventType = EventType.KafkaAdd;
        }
    }
}
