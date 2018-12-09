using Grpc.Service.Core.AutoMapper;
using Grpc.Service.Core.Caching;
using Grpc.Service.Core.Domain.Reporting;
using Newtonsoft.Json;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class TradeReportDatabase : IReportDatabase
    {
        private readonly TradeRepository _repository;
        private readonly IncomeTradeRepository _incomeRepository;
        private readonly ConsumeTradeRepository _consumeRepository;
        private readonly CashApplyRepository _cashRepository;
        private readonly ICacheManager _cacheManager;
        public TradeReportDatabase(TradeRepository repository,IncomeTradeRepository incomeRepository,
            ConsumeTradeRepository consumeRepository, CashApplyRepository cashRepository, 
            ICacheManager cacheManager)
        {
            _repository = repository;
            _incomeRepository = incomeRepository;
            _consumeRepository = consumeRepository;
            _cashRepository = cashRepository;
            _cacheManager = cacheManager;
        }        

        public bool Add(TradeEntity item)
        {
            return _repository.AddTrade(item);
        }
        public bool Add(ComTradeEntity item)
        {
            return _incomeRepository.AddTrade(item);
        }
        public bool Add(ConsumeTradeEntity item)
        {
            return _consumeRepository.AddTrade(item);
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
        public TradeDomain GetTradeByShipOrderId(int shipOrderId)
        {
            var entity = _repository.GetTradeByShipOrderId(shipOrderId);
            
            return ConvertEntityToDomain(entity);
        }

        public List<TradeDomain> GetTradeHistoryList(string accountId, long pageIndex, long pageSize)
        {
            var cache = _cacheManager.GetCache<string, string>("CacheItems");
            var domainList = new List<TradeDomain>();
            if (cache != null)
            {
                long count = cache.ListLength("trad:" + accountId);
                long pstart = (pageIndex - 1) * pageSize;
                long pend = (pstart == 0? (pageSize-1) : (pageIndex * pageSize-1));
                if(pstart > count)
                {
                    return domainList;
                }
                if (pend > count)
                {
                    pend = count;
                }
                    var list = cache.ListRange("trad:" + accountId, pstart, pend);
                if (list != null && list.Count() > 0)
                {
                    System.Console.WriteLine("redissKey:" + "trad:" + accountId);
                    SetDomainList(domainList, list);
                }
                else
                {
                    var minDate = GetMinDate(accountId);
                    
                    while (minDate <DateTime.Now)
                    {                        
                        var start = DateTime.Parse(minDate.ToShortDateString());
                        var end = start.AddDays(1);
                        SetTradeList(accountId, start, end, cache);
                        minDate = end;
                    }
                    var retlist = cache.ListRange("trad:" + accountId, pstart, pend);
                    if (retlist != null)
                    {
                        SetDomainList(domainList, retlist);
                    }
                }
            }
            return domainList;
        }
        public long GetTradeHistoryCount(string accountId)
        {
            var cache = _cacheManager.GetCache<string, string>("CacheItems");
            long count = 0;
            if (cache != null)
            {
                count = cache.ListLength("trad:"+accountId);
            }
            return count;
        }

        private  DateTime GetMinDate(string accountId)
        {
            var dateList = new List<DateTime>();
            var cashDate = _cashRepository.GetMinDate(accountId);
            if (cashDate != null)
            {
                dateList.Add(cashDate.Value);                
            }
            var incomeDate = _incomeRepository.GetMinDate(accountId);
            if (incomeDate != null)
            {
                dateList.Add(incomeDate.Value);                
            }
            var consumeDate = _consumeRepository.GetMinDate(accountId);
            if (consumeDate != null)
            {
                dateList.Add(consumeDate.Value);                
            }
            var minDate = ((dateList == null || dateList.Count == 0) ? DateTime.Now : dateList.Min());
            return minDate;
        }
        private void SetTradeList(string accountId,DateTime start , DateTime end, ITypedCache<string, string> cache)
        {            
            var tradeList = new List<TradeDomain>();
            var cashList = _cashRepository.GetTradeList(accountId, start, end);
            foreach (var item in cashList)
            {
                tradeList.Add(item.ToDomain<TradeDomain>());
            }
            var incomeList = _incomeRepository.GetTradeList(accountId, start, end);
            foreach (var item in incomeList)
            {                
                var domain = AutoMapper.Mapper.Map<ComTradeEntity, TradeDomain>(item);
                tradeList.Add(domain);
            }
            var consumeList = _consumeRepository.GetTradeList(accountId, start, end);
            foreach (var item in consumeList)
            {
                tradeList.Add(item.ToDomain<TradeDomain>());
            }            
            tradeList = tradeList.OrderBy(x => x.CreateTime).ToList();
            foreach (var item in tradeList)
            {
                cache.ListLeftPush("trad:" + accountId, item.GetMementoJson());
            }
        }
        private void SetDomainList(List<TradeDomain> domainList,object[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                var entity = JsonConvert.DeserializeObject<TradeDomain>(list[i].ToString());
                domainList.Add(entity);
            }
        }

        private TradeDomain ConvertEntityToDomain(TradeFullEntity entity)
        {
            var order = new TradeDomain();
            order.SetMemento(entity);
            return order;
        }
        private TradeDomain ConvertEntityToDomain(TradeEntity entity)
        {
            if(entity== null)
            {
                return null;
            }
            var order = new TradeDomain();
            order.SetBaseMemento(entity);
            return order;
        }
    }
}
