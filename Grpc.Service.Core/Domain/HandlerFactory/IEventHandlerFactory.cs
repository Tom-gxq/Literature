using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.HandlerFactory
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event;
    }
}
