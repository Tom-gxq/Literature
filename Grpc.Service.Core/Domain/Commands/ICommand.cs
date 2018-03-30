using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Commands
{
    public interface ICommand
    {
        Guid Id { get; set; }
    }
}
