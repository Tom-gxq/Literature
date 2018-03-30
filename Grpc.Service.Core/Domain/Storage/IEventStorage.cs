using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Storage
{
    public interface IEventStorage
    {
        IEnumerable<Event> GetEvents(Guid aggregateId);
        void Save(AggregateRoot<Guid> aggregate);
    }
}
