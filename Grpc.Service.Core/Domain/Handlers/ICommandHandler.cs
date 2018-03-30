using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Handlers
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<TCommand> :  ICommandHandler
        where TCommand : class, ICommand
    {
        void Execute(TCommand command);
    }
}
