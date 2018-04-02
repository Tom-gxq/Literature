using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Messaging
{
    public class EventBus : IEventBus
    {
        private IEventHandlerFactory _eventHandlerFactory;

        public EventBus(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Publish<T>(T @event) where T : Event
        {
            var handlers = _eventHandlerFactory.GetHandlers<T>();
            foreach (var eventHandler in handlers)
            {
                eventHandler.Handle(@event);
            }
        }
    }
}
