using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string message) : base(message) { }
    }
}
