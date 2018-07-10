using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Sender;
using SP.Service.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class KafkaAddEventHandler : IEventHandler<KafkaAddEvent>
    {
        public KafkaAddEventHandler()
        {
            
        }
        public void Handle(KafkaAddEvent handle)
        {
            SP.Producer.Util.Sender.Add(handle.Prducer);
        }
    }
}
