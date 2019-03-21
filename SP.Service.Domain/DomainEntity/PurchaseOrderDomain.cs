using Grpc.Service.Core.Domain.Entity;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class PurchaseOrderDomain : AggregateRoot<Guid>
    {
        public string OrderId { get; set; }
        public string OrderCode { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PayDate { get; set; }
        public int PayType { get; set; }
        public int OrderStatus { get; set; }
        public AccountInfoDomain AccountInfo { get; set; }        
        public AccountAddressDomain Address { get; set; }
        public List<ShoppingCartsDomain> ShoppingCarts { get; set; }
        public PurchaseOrderDomain()
        {

        }

        public void SetAccountInfoMemento(BaseEntity memento)
        {
            if (memento != null)
            {
                var entity = memento as AccountInfoEntity;
                AccountInfo = new AccountInfoDomain()
                {
                    Fullname = entity?.Fullname ?? string.Empty,
                    AccountId = entity?.AccountId
                };
            }
        }
        public override void SetMemento(BaseEntity memento)
        {
            if (memento != null)
            {
                var entity = memento as OrdersEntity;
                this.OrderId = entity.OrderId;
                this.OrderCode = entity.OrderCode;
                this.OrderDate = entity.OrderDate != null ? entity.OrderDate.Value : DateTime.MinValue;
                this.PayType = entity.IsWxPay.Value ? 1 : (entity.IsAliPay.Value?2:0);
                this.OrderStatus = entity.OrderStatus.Value;
                this.PayDate = entity.PayDate != null ? entity.PayDate.Value : DateTime.MinValue;
                this.Amount = entity.Amount != null ? entity.Amount.Value:0;
            }
        }
    }
}
    
