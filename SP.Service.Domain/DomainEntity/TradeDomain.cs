using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SP.Service.Domain.Events;
using SP.Service.Domain.Util;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class TradeDomain : AggregateRoot<Guid>, IHandle<TradeCreateEvent>, IHandle<CashApplyCreatedEvent>
    {
        public string AccountId { get;  set; }
        public int Subject { get;  set; }
        public double Amount { get;  set; }
        public DateTime CreateTime { get;  set; }
        public int ShipOrderId { get;  set; }
        public string Alipay { get; set; }
        public double Money { get; set; }
        public double BalanceAmount{ get; set; }
        public string TradeNo { get; set; }
        public string TradeId { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }
        public string Sign { get; set; }
        public int TradeType { get; set; }

        public TradeDomain()
        {
            
        }
        public TradeDomain(Guid id,string accountId, IncomeTradeEnum subject,double amount,string productId,int shipOrderId=0)
        {
            this.Id = id;
            this.AccountId = accountId;
            this.TradeNo = DateTime.Now.ToString("yyyyMMddH24mmssttt");
            this.Subject = (int)subject;
            this.Amount = amount;
            this.ShipOrderId = shipOrderId;
            this.ProductId = productId;
            //ApplyChange(new TradeCreateEvent(Guid.NewGuid(), accountId, cartId,subject, amount, shipOrderId));
        }

        public void CreateCashApply()
        {
            ApplyChange(new CashApplyCreatedEvent(this.Id, this.AccountId, this.Alipay, this.Money));
        }
        public void CreateIncomeTrade()
        {
            ApplyChange(new IncomeTradeCreateEvent(this.Id, this.AccountId, this.TradeNo, this.BalanceAmount,
               this.Amount,this.Subject,this.ShipOrderId,this.ProductId));
        }
        public void CreateConsumeTrade()
        {
            ApplyChange(new ConsumeTradeCreateEvent(this.Id, this.AccountId, this.TradeNo, this.BalanceAmount,
                 this.Amount, this.OrderId, this.Sign));
        }
        public BaseEntity GetMemento()
        {
            return new TradeEntity()
            {
                AccountId = this.AccountId,
                Subject = this.Subject,
                TradeId = this.Id.ToString(),
                Amount = this.Amount,
                CreateTime = this.CreateTime
            };
        }

        public string GetMementoJson()
        {
            JObject requestData = new JObject{
                {"AccountId",this.AccountId},
                {"CreateTime",this.CreateTime},
                {"TradeNo",this.TradeNo},
                {"BalanceAmount",this.BalanceAmount},
                {"TradeType",this.TradeType},
                };
            return JsonConvert.SerializeObject(requestData);
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is TradeFullEntity)
            {
                var entity = memento as TradeFullEntity;
                this.AccountId = entity.AccountId;
                this.Subject = entity.Subject.Value;
                this.Id = new Guid(entity.TradeId);
                this.Amount = entity.Amount != null ? entity.Amount.Value:0;
                this.CreateTime = entity.CreateTime.Value;
            }
        }
        public void SetBaseMemento(BaseEntity memento)
        {
            if (memento is TradeEntity)
            {
                var entity = memento as TradeEntity;
                this.AccountId = entity.AccountId;
                this.Subject = entity.Subject.Value;
                this.Id = new Guid(entity.TradeId);
                this.Amount = entity.Amount != null ? entity.Amount.Value : 0;
                this.CreateTime = entity.CreateTime.Value;
            }
        }
        public void Handle(TradeCreateEvent e)
        {
            this.AccountId = e.AccountId;
            this.Subject = e.Subject;
            this.Amount = e.Amount;
        }
        public void Handle(CashApplyCreatedEvent e)
        {
            this.AccountId = e.AccountId;
            this.Alipay = e.Alipay;
            this.Money = e.Money;
        }
    }
}
