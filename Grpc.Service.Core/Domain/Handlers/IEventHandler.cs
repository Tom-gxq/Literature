using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Handlers
{
    public interface IEventHandler
    {

    }
    public interface IEventHandler<TEvent>: IEventHandler
        where TEvent : Event
    {
        void Handle(TEvent handle);
    }
}
