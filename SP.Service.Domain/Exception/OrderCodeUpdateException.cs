using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain
{
    public class OrderCodeUpdateException : Exception
    {
        public string orderCode { get; internal set; }
        public OrderCodeUpdateException(string ordeCode)
        {
            this.orderCode = ordeCode;
        }
    }
}
