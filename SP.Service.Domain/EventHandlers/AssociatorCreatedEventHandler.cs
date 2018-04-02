using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AssociatorCreatedEventHandler : IEventHandler<AssociatorCreatedEvent>
    {
        private readonly AssociatorReportDatabase _reportDatabase;
        private readonly SysKindReportDatabase _kindReportDatabase;
        public AssociatorCreatedEventHandler(AssociatorReportDatabase reportDatabase, SysKindReportDatabase kindReportDatabase)
        {
            _reportDatabase = reportDatabase;
            _kindReportDatabase = kindReportDatabase;
        }
        public void Handle(AssociatorCreatedEvent handle)
        {
            var item = new AssociatorEntity()
            {
                AssociatorId = handle.AggregateId.ToString(),
                AccountId = handle.AccountId,
                KindId = handle.KindId,
                Amount = handle.Amount,
                PayOrderCode = handle.PayOrderCode,
                PayType = handle.PayType,
                StartDate = DateTime.Now,
                EndDate = GetEndDate(handle.KindId, handle.Quantity),
                Quantity = handle.Quantity,
                Status = 1
            };
            _reportDatabase.Add(item);
        }

        private DateTime GetEndDate(string kindId,int orderQuantity)
        {
            var domain = _kindReportDatabase.GetSysKindById(kindId);
            DateTime endate = DateTime.MinValue;
            switch(domain.Unit)
            {
                case 1://日
                    endate = DateTime.Now.AddDays(orderQuantity * domain.Quantity);
                    break;
                case 2://月
                    endate = DateTime.Now.AddMonths(orderQuantity * domain.Quantity);
                    break;
                case 3://年
                    endate = DateTime.Now.AddYears(orderQuantity * domain.Quantity);
                    break;
            }
            return endate;
        }
    }
}
