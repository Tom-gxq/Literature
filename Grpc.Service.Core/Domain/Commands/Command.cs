using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Commands
{
    public abstract class Command : ICommand
    {
        public Command()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id
        {
            get; set;
        }
    }
}
