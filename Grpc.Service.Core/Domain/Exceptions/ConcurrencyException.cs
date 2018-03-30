using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message) : base(message) { }
    }
}
