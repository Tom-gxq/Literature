using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ApplyPartnerCreatedEventHandler : IEventHandler<ApplyPartnerCreatedEvent>
    {
        private readonly ApplyPartnerReportDatabase _reportDatabase;
        public ApplyPartnerCreatedEventHandler(ApplyPartnerReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(ApplyPartnerCreatedEvent handle)
        {
            _reportDatabase.Add(new Entity.ApplyPartnerEntity()
            {
                 AccountId = handle.AggregateId.ToString(),
                 DormId = handle.DormId,
                 Status = 0,
                 CreateTime = DateTime.Now
            });
        }
    }
}
