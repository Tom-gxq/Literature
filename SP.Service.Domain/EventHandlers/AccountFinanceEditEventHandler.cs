using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountFinanceEditEventHandler : IEventHandler<HaveAmountEditEvent>, IEventHandler<UseAmountEditEvent>
    {
        private readonly AccountFinanceReportDatabase _reportDatabase;
        private object lockObj = new object();
        public AccountFinanceEditEventHandler(AccountFinanceReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(HaveAmountEditEvent handle)
        {
            lock (lockObj)
            {
                var entity = _reportDatabase.GetAccountFinanceDetail(handle.AccountId);
                if (entity != null)
                {
                    var item = new AccountFinanceEntity()
                    {
                        AccountId = handle.AccountId,
                        HaveAmount = entity.HaveAmount + handle.Amount
                    };

                    _reportDatabase.UpdateAccountFinance(item);
                }
            }
        }
        public void Handle(UseAmountEditEvent handle)
        {
            lock (lockObj)
            {
                var entity = _reportDatabase.GetAccountFinanceDetail(handle.AccountId);
                if (entity != null)
                {
                    var item = new AccountFinanceEntity()
                    {
                        AccountId = handle.AccountId,
                        UseAmount = entity.UseAmount + handle.Amount
                    };

                    _reportDatabase.UpdateAccountFinance(item);
                }
            }
        }
    }
}
