using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class BalancePayEventHandler : IEventHandler<BalancePayEvent>
    {
        private readonly BalancePayReportDatabase _reportDatabase;
        public BalancePayEventHandler(BalancePayReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(BalancePayEvent handle)
        {
            _reportDatabase.Pay(handle.AccountId, handle.OrderCode, handle.PassWord,handle.Amount, handle.AggregateId.ToString(),handle.Sign);
        }
    }
}
