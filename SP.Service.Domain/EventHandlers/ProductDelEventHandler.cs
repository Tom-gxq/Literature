using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ProductDelEventHandler : IEventHandler<ProductDelEvent>
    {
        private readonly ProductReportDatabase _reportDatabase;
        public ProductDelEventHandler(ProductReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(ProductDelEvent handle)
        {
           _reportDatabase.Del(handle.AggregateId.ToString());
        }
    }
}
