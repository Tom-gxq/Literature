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
    public class SumSellerStatisticsCommandHandler:ICommandHandler<SumSellerStatisticsCommand>
    {
        private IDataRepository<SellerStatisticsDomain> _repository;
        private SellerStatisticsReportDatabase _statisticsReportDatabase;

        public SumSellerStatisticsCommandHandler(IDataRepository<SellerStatisticsDomain> repository,
            SellerStatisticsReportDatabase statisticsReportDatabase)
        {
            this._repository = repository;
            this._statisticsReportDatabase = statisticsReportDatabase;
        }

        public void Execute(SumSellerStatisticsCommand command)
        {
            SellerStatisticsDomain aggregate = null;
            DateTime now = DateTime.Parse(DateTime.Now.ToShortDateString());
            Console.WriteLine($"SumSellerStatisticsCommand now={now.ToString()} ShippingId={command.ShippingId}");
            var entity = _statisticsReportDatabase.GetTodayStatistic(now, command.ShippingId);
            
            if (entity != null && !string.IsNullOrEmpty(entity.SSID))
            {
                aggregate = new SellerStatisticsDomain();
                aggregate.SumOrderStatistics(entity.SSID, command.Shipto, command.OrderId, command.OrderAmount);
            }
            else
            {
                aggregate = new SellerStatisticsDomain(command.Id, command.ShippingId, command.Shipto, command.OrderId, command.OrderAmount, now);
            }
            if (aggregate != null)
            {
                _repository.Save(aggregate);
            }

        }
    }
}
