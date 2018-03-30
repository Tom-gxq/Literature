using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Domain.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.HandlerFactory
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : Command;
    }
}
