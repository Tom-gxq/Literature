using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AssociatorEditEventHandler : IEventHandler<AssociatorEditEvent>
    {
        private readonly AssociatorReportDatabase _reportDatabase;
        public AssociatorEditEventHandler(AssociatorReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AssociatorEditEvent handle)
        {
            var item = new AssociatorEntity()
            {
                AssociatorId = handle.AggregateId.ToString(),
                Status = handle.Status
            };

            _reportDatabase.UpdateAssociator(item);
        }
    }
}
