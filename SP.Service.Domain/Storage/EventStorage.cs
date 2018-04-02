using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.Exceptions;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Messaging;
using Grpc.Service.Core.Domain.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.Domain.Storage
{
    public class EventStorage : IEventStorage
    {
        private List<Event> _events;

        private readonly IEventBus _eventBus;

        public EventStorage(IEventBus eventBus)
        {
            _events = new List<Event>();
            _eventBus = eventBus;
        }

        public IEnumerable<Event> GetEvents(Guid aggregateId)
        {
            var events = _events.Where(p => p.AggregateId == aggregateId).Select(p => p);
            if (events.Count() == 0)
            {
                throw new AggregateNotFoundException(string.Format("Aggregate with Id: {0} was not found", aggregateId));
            }
            return events;
        }

        public void Save(AggregateRoot<Guid> aggregate)
        {
            var uncommittedChanges = aggregate.GetUncommittedChanges();
            var version = aggregate.Version;

            foreach (var @event in uncommittedChanges)
            {
                version++;                
                @event.Version = version;
                _events.Add(@event);
            }
            foreach (var @event in uncommittedChanges)
            {
                var desEvent = Converter.ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }
        }
    }
}
