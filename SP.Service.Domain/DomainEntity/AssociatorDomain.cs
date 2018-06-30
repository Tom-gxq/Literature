using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using SP.Producer;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class AssociatorDomain : AggregateRoot<Guid>, IHandle<AssociatorCreatedEvent>, IHandle<AssociatorEditEvent>,
        IHandle<KafkaAddEvent>,        IOriginator
    {
        public string AccountId { get; set; }
        public string KindId { get; set; }
        public int Quantity { get; set; }
        public string PayOrderCode { get; set; }
        public int PayType { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }

        public AssociatorDomain()
        {

        }
        public AssociatorDomain(Guid associatorId, string accountId, string kindId, int quantity,
            string payOrderCode, int payType, double amount)
        {
            ApplyChange(new AssociatorCreatedEvent(associatorId,accountId, kindId, quantity, payOrderCode, payType, amount));
        }
        public void EditAssociatorDomain(Guid associatorId,int status)
        {
            ApplyChange(new AssociatorEditEvent(associatorId, status));
        }
        public void AddMemberKafkaInfo(string accountId, double amount, AuthorizeType Type)
        {
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();
            string kafkaIP = string.Empty;
            if (config != null)
            {
                kafkaIP = config.GetSection("KafkaIP").Value?.ToString() ?? string.Empty;
            }
            var producer = new KafkaMemberProducer();
            producer.IPConfig = kafkaIP;
            producer.AccountId = accountId;
            producer.Amount = amount;
            producer.Type = Type;
            ApplyChange(new KafkaAddEvent(producer));
        }
        public BaseEntity GetMemento()
        {
            return new AssociatorEntity()
            {
                AssociatorId = this.Id.ToString(),
                AccountId = this.AccountId,
                KindId = this.KindId,
                Amount = this.Amount,
                PayOrderCode = this.PayOrderCode,
                PayType = this.PayType,
                StartDate = DateTime.Now,
                EndDate = GetEndDate(),
                Quantity = this.Quantity,
                Status = 1
            };
        }
        private DateTime GetEndDate()
        {
            return DateTime.Now;
        }

        public void Handle(AssociatorCreatedEvent e)
        {
            this.Id = e.AggregateId;
            this.AccountId = e.AccountId;
            this.Amount = e.Amount;
            this.KindId = e.KindId;
            this.PayOrderCode = e.PayOrderCode;
            this.PayType = e.PayType;
            this.Quantity = e.Quantity;
        }
        public void Handle(AssociatorEditEvent e)
        {
            this.Id = e.AggregateId;
            this.Status = e.Status;
        }

        public void Handle(KafkaAddEvent e)
        {

        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is AssociatorEntity)
            {
                var entity = memento as AssociatorEntity;
                this.AccountId = entity.AccountId;
                this.Id = new Guid(entity.AssociatorId);
                this.KindId = entity.KindId;
                this.Amount = entity.Amount.Value;
                this.PayOrderCode = entity.PayOrderCode;
                this.PayType = entity.PayType.Value;
                this.StartDate = entity.StartDate.Value;
                this.EndDate = entity.EndDate.Value;
                this.Quantity = entity.Quantity.Value;
                this.Status = entity.Quantity != null ? entity.Quantity.Value : 0;
            }
        }
    }
}
