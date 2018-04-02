using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.Domain.Storage
{
    public class DataRepository<T> : IDataRepository<T> where T : AggregateRoot<Guid>, new()
    {
        private readonly IEventStorage _storage;
        private static object _lockStorage = new object();

        public DataRepository(IEventStorage storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot<Guid> aggregate, int expectedVersion)
        {
            if (aggregate.GetUncommittedChanges().Any())
            {
                lock (_lockStorage)
                {
                    _storage.Save(aggregate);
                }
            }
        }

        public T GetById(Guid id)
        {
            IEnumerable<Event> events;
            events = _storage.GetEvents(id);
            var obj = new T();
            
            obj.LoadsFromHistory(events);
            return obj;
        }
    }
}
