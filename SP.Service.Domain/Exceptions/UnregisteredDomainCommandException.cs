using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Exceptions
{
    public class UnregisteredDomainCommandException : Exception
    {
        public UnregisteredDomainCommandException(string message) : base(message) { }
    }
}
