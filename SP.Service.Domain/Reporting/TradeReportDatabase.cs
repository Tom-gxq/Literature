using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class TradeReportDatabase : IReportDatabase
    {
        private readonly TradeRepository _repository;
        public TradeReportDatabase(TradeRepository repository)
        {
            _repository = repository;
        }        

        public bool Add(TradeEntity item)
        {
            return _repository.AddTrade(item);
        }

        public List<TradeDomain> GetTradeList(string accountId, int pageIndex, int pageSize)
        {
            var list =  _repository.GetTradeList(accountId, pageIndex, pageSize);
            var domainList = new List<TradeDomain>();
            list.ForEach(x=> domainList.Add(ConvertEntityToDomain(x)));
            return domainList;
        }
        public long GetTradeListCount(string accountId)
        {
            var count = _repository.GetTradeListCount(accountId);
            return count;
        }

        public double GetLatelyTrade(string accountId)
        {
            var list = _repository.GetLatelyTrade(accountId);
            double amonut = 0;
            list.ForEach(x => amonut+=(x.Amount == null ? 0 : x.Amount.Value));
            return amonut;
        }
        private TradeDomain ConvertEntityToDomain(TradeFullEntity entity)
        {
            var order = new TradeDomain();
            order.SetMemento(entity);
            return order;
        }
    }
}
