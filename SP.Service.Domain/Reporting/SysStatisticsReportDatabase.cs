using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class SysStatisticsReportDatabase : IReportDatabase
    {
        private readonly SysStatisticsRepository _repository;
        public SysStatisticsReportDatabase(SysStatisticsRepository repository)
        {
            _repository = repository;
        }

        public SysStatisticsDomain GetTodaySysStatistic(DateTime createTime)
        {
            var order = _repository.GetTodaySysStatistic(createTime);

            return ConvertSysStatisticsToDomain(order);
        }

        public void Add(SysStatisticsEntity item)
        {
            _repository.AddSysStatistic(item);
        }
        public bool UpdateOrderStatistic(string accountId, DateTime createTime, double foodAmount, double markAmount)
        {
            var result = _repository.UpdateOrderStatistic(createTime, foodAmount, markAmount);
            return result;
        }
        public bool UpdateNewUserStatistic(DateTime createTime)
        {
            var result = _repository.UpdateNewUserStatistic(createTime);
            return result;
        }
        public bool UpdateNewAssociatorStatistic(DateTime createTime)
        {
            var result = _repository.UpdateNewAssociatorStatistic(createTime);
            return result;
        }
        public bool UpdateBuyAssociatorStatistic(DateTime createTime)
        {
            var result = _repository.UpdateBuyAssociatorStatistic(createTime);
            return result;
        }
        public bool UpdateOrderStatisticCheckedStatus(DateTime createTime, bool isChecked)
        {
            var result = _repository.UpdateOrderStatisticCheckedStatus(createTime, isChecked);
            return result;
        }
        private SysStatisticsDomain ConvertSysStatisticsToDomain(SysStatisticsEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var order = new SysStatisticsDomain();
            order.SetMemento(entity);


            return order;
        }
    }
}
