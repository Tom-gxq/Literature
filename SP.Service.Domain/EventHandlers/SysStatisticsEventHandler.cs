using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class SysStatisticsEventHandler : IEventHandler<SysStatisticsSumOrderEvent>, IEventHandler<SysStatisticsCreateEvent>,
        IEventHandler<SysStatisticsSumBuyMemberEvent>, IEventHandler<SysStatisticsSumNewMemberEvent>, IEventHandler<SysStatisticsSumUserEvent>
    {
        private readonly SysStatisticsReportDatabase _reportDatabase;
        public SysStatisticsEventHandler(SysStatisticsReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }

        public void Handle(SysStatisticsCreateEvent handle)
        {
            var item = new SysStatisticsEntity()
            {
                Num_BuyAssociator = handle.Num_BuyAssociator,
                Num_OrderAmount = handle.Num_OrderAmount,
                CreateTime = handle.CreateTime,
                Num_NewAssociator = handle.Num_NewAssociator,
                Num_NewOrder = handle.Num_NewOrder,
                Num_NewUser = handle.Num_NewUser
            };

            _reportDatabase.Add(item);
        }
        public void Handle(SysStatisticsSumOrderEvent handle)
        {
            _reportDatabase.UpdateOrderStatistic(handle.AccountId,handle.OrderDate,handle.Amount);
        }
        public void Handle(SysStatisticsSumBuyMemberEvent handle)
        {
            _reportDatabase.UpdateBuyAssociatorStatistic(handle.CreateTime);
        }
        public void Handle(SysStatisticsSumNewMemberEvent handle)
        {
            _reportDatabase.UpdateNewAssociatorStatistic(handle.CreateTime);
        }
        public void Handle(SysStatisticsSumUserEvent handle)
        {
            _reportDatabase.UpdateNewUserStatistic(handle.CreateTime);
        }
    }
}
