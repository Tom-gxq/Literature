using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class BalancePayDomain : AggregateRoot<Guid>,
        IHandle<BalancePayEvent>
    {
        public string Token { get; set; }
        public string PassWord { get; set; }
        public string OrderCode { get; set; }
        public double Amount { get; set; }
        public string Sign { get; set; }
        public string AccountId { get; set; }

        public BalancePayDomain()
        {
            
        }
        public void Create()
        {
            var @event = new BalancePayEvent(this.Id, this.AccountId, this.OrderCode, this.PassWord, this.Amount, this.Sign);
            
            ApplyChange(@event);
        }
        public void Handle(BalancePayEvent e)
        {
            this.Token = e.AggregateId.ToString();
            this.PassWord = e.PassWord;
            this.Amount = e.Amount;
            this.OrderCode = e.OrderCode;
            this.Sign = e.Sign;
        }
    }
}
