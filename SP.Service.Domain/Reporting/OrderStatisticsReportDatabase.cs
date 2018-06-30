using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class OrderStatisticsReportDatabase : IReportDatabase
    {
        private readonly ShipStatisticsRepository _repository;
        public OrderStatisticsReportDatabase(ShipStatisticsRepository repository)
        {
            _repository = repository;
        }

        public OrderStatisticsDomain GetTodayOrderStatistic(string accountId, int dormId, DateTime createTime)
        {
            var order = _repository.GetTodayOrderStatistic( accountId,  dormId, createTime);

            return ConvertShipStatisticsToDomain(order);
        }
        public void Add(ShipStatisticsEntity item)
        {
            _repository.AddOrderStatistic(item);
        }
        public bool UpdateOrderStatistic(string accountId, int dormId, DateTime createTime, double amount)
        {
            var result = _repository.UpdateOrderStatistic(accountId, dormId, createTime, amount);
            return result;
        }

        private OrderStatisticsDomain ConvertShipStatisticsToDomain(ShipStatisticsEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var order = new OrderStatisticsDomain();
            order.SetMemento(entity);
            

            return order;
        }
    }
}
