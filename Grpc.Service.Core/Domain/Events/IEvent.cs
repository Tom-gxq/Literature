using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Events
{
    public interface IEvent
    {
        int Version { get; set; }
    }
}
