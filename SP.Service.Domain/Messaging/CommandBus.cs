using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Messaging;
using SP.Service.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandBus(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public void Send<T>(T command) where T : Command
        {
            var handler = _commandHandlerFactory.GetHandler<T>();
            if (handler != null)
            {
                handler.Execute(command);
            }
            else
            {
                throw new UnregisteredDomainCommandException("no handler registered");
            }
        }
    }
}
