using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Statistics;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class SumOrderStatisticsCommandHandler : ICommandHandler<SumOrderStatisticsCommand>
    {
        private IDataRepository<OrderStatisticsDomain> _repository;
        private IDataRepository<SysStatisticsDomain> _sysRepository;
        private OrderStatisticsReportDatabase _statisticsReportDatabase;
        private SysStatisticsReportDatabase _sysStatisticsReportDatabase;

        public SumOrderStatisticsCommandHandler(IDataRepository<OrderStatisticsDomain> repository, IDataRepository<SysStatisticsDomain> sysRepository,
            OrderStatisticsReportDatabase statisticsReportDatabase,
            SysStatisticsReportDatabase sysStatisticsReportDatabase)
        {
            this._repository = repository;
            this._sysRepository = sysRepository;
            this._statisticsReportDatabase = statisticsReportDatabase;
            this._sysStatisticsReportDatabase = sysStatisticsReportDatabase;
        }

        public void Execute(SumOrderStatisticsCommand command)
        {
            OrderStatisticsDomain aggregate = null;
            
            var entity = _statisticsReportDatabase.GetTodayOrderStatistic(command.AccountId, command.AddressId, command.OrderDate);
            if (entity != null && !string.IsNullOrEmpty(entity.AccountId))
            {
                aggregate = new OrderStatisticsDomain();
                aggregate.SumOrderStatistics(command.OrderId, command.OrderCode, command.OrderDate,
                    command.AccountId, command.Amount, command.AddressId);
            }
            else
            {
                aggregate = new OrderStatisticsDomain(command.OrderId, command.OrderCode, command.OrderDate,
                    command.AccountId, command.Amount, command.AddressId);
            }
            if (aggregate != null)
            {
                _repository.Save(aggregate);
            }

            SysStatisticsDomain sysStatisticsDomain = null;
            var sysEntity = _sysStatisticsReportDatabase.GetTodaySysStatistic(command.OrderDate);
            if (sysEntity != null && sysEntity.CreateTime >DateTime.MinValue)
            {
                sysStatisticsDomain = new SysStatisticsDomain();
                sysStatisticsDomain.SumOrderStatistics(command.OrderId, command.OrderCode, command.OrderDate,
                    command.AccountId, command.Amount, command.AddressId);
            }
            else
            {
                sysStatisticsDomain = new SysStatisticsDomain(command.OrderDate, 0, 0,0,1, command.Amount);
            }
            if (sysStatisticsDomain != null)
            {
                _sysRepository.Save(sysStatisticsDomain);
            }

        }
    }
}
