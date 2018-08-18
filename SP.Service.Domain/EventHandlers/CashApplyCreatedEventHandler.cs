using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class CashApplyCreatedEventHandler : IEventHandler<CashApplyCreatedEvent>
    {
        private readonly CashApplyReportDatabase _reportDatabase;
        public CashApplyCreatedEventHandler(CashApplyReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(CashApplyCreatedEvent handle)
        {
            var item = new CashApplyEntity()
            {
                AccountId = handle.AccountId,
                Alipay = handle.Alipay,
                Money = handle.Money,
                CreateTime = DateTime.Now,
                Status = 0
            };

            _reportDatabase.Add(item);
        }
    }
}
