using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grpc.Service.Core.Domain.Entity
{
    public abstract class AggregateRoot<TKey> : IAggregateRoot<TKey>,IOriginator
        where TKey : IEquatable<TKey>
    {
        private readonly List<Event> _changes;

        public TKey Id { get; set; }
        public int Version { get; internal set; }
        public int EventVersion { get; protected set; }
        public DaomainAction Action { get; internal set; }

        protected AggregateRoot()
        {
            Action = DaomainAction.Create;
            _changes = new List<Event>();
        }

        protected void SetAction(DaomainAction action)
        {
            this.Action = action;
        }
        public void Replay(IEnumerable<Event> events)
        {
            if (this._changes != null && this._changes.Count > 0)
            {
                this._changes.Clear();
            }
            foreach (var evnt in events)
            {
                this.ApplyChange(evnt);
            }
        }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
            Version = history.Last().Version;
            EventVersion = Version;
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            dynamic d = this;

            d.Handle(Converter.ChangeTo(@event, @event.GetType()));
            if (isNew)
            {
                _changes.Add(@event);
            }
        }
        public virtual BaseEntity GetMemento()
        {
            return null;
        }
        public virtual void SetMemento(BaseEntity memento)
        {

        }
    }
}
