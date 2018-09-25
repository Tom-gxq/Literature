using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class SellerStatisticsReportDatabase : IReportDatabase
    {
        private readonly SellerStatisticsRepository _repository;
        private readonly SellerStatisticsTradeRepository _tradeRepository;
        public SellerStatisticsReportDatabase(SellerStatisticsRepository repository, SellerStatisticsTradeRepository tradeRepository)
        {
            _repository = repository;
            _tradeRepository = tradeRepository;
        }

        public SellerStatisticsDomain GetTodayStatistic(DateTime createTime,string accountId)
        {
            var order = _repository.GetTodayStatistic(createTime, accountId);

            return ConvertStatisticsToDomain(order);
        }

        public void Add(SellerStatisticsEntity item)
        {
            _repository.AddStatistic(item);
        }
        public void AddTrade(SellerStatisticsTradeEntity item)
        {
            _tradeRepository.AddStatisticTrade(item);
        }
        public bool UpdateOrderStatistic(string id, double amount)
        {
            var result = _repository.UpdateOrderStatistic(id, amount);
            return result;
        }
        
        public bool UpdateOrderStatisticCheckedStatus(DateTime createTime, bool isChecked)
        {
            var result = _repository.UpdateOrderStatisticCheckedStatus(createTime, isChecked);
            return result;
        }
        private SellerStatisticsDomain ConvertStatisticsToDomain(SellerStatisticsEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var order = new SellerStatisticsDomain();
            order.SetMemento(entity);


            return order;
        }
    }
}
