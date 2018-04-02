using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Dependency;
using SP.Service.Domain.Messaging;

namespace SP.Service.Domain.HandlerFactory
{
    public class CommandHandlerFactor : ICommandHandlerFactory
    {
        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            var handlers = GetHandlerTypes<T>().ToList();

            var cmdHandler = handlers.Select(handler =>
                (ICommandHandler<T>)IocManager.Instance.Resolve(handler)).FirstOrDefault();

            return cmdHandler;
        }

        private IEnumerable<Type> GetHandlerTypes<T>() where T : Command
        {
            var handlers = typeof(CommandBus).GetTypeInfo().Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.GetTypeInfo().IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                    .Where(h => h.GetInterfaces()
                        .Any(ii => ii.GetGenericArguments()
                            .Any(aa => aa == typeof(T)))).ToList();

            

            var af = typeof(CommandBus).GetTypeInfo().Assembly.GetExportedTypes();
            foreach(var item in af)
            {
                var face = item.GetInterfaces();
                foreach(var faceItem in face)
                {
                    var gtype = faceItem.GetTypeInfo().IsGenericType;
                    try
                    {
                        var gtydef = faceItem.GetGenericTypeDefinition();
                    }
                    catch
                    {

                    }
                }
            }
            return handlers;
        }

    }
}
