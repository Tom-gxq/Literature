using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Dependency;
using SP.Service.Domain.Messaging;

namespace SP.Service.Domain.HandlerFactory
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event
        {
            var handlers = GetHandlerType<T>();

            var lstHandlers = handlers.Select(handler => (IEventHandler<T>)IocManager.Instance.Resolve(handler)).ToList();
            return lstHandlers;
        }

        private static IEnumerable<Type> GetHandlerType<T>() where T : Event
        {
            var handlers = typeof(EventBus).GetTypeInfo().Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.GetTypeInfo().IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                    .Where(h => h.GetInterfaces()
                        .Any(ii => ii.GetGenericArguments()
                        .Any(aa => aa == typeof(T)))).ToList();

            return handlers;
        }
    }
}
