using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Events
{
    public interface IEventExecuteHandler
    {
        void ExecuteEvent(string text);
    }
}
