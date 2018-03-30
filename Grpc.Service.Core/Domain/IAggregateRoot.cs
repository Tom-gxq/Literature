using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Reapplies the events on current aggregate root.
        /// </summary>
        /// <param name="events">The events.</param>
        void Replay(IEnumerable<Event> events);
        void LoadsFromHistory(IEnumerable<Event> history);
        IEnumerable<Event> GetUncommittedChanges();
    }
}
