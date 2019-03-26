using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class SellerProductDomain : AggregateRoot<Guid>, IHandle<SellerProductCreatedEvent>, IHandle<SellerProductDelEvent>
    {
        public string AccountId { get; internal set; }
        public int SupplierProductId { get; internal set; }

        public SellerProductDomain()
        {

        }
        public SellerProductDomain(Guid id, string accountId, int supplierProductId)
        {
            ApplyChange(new SellerProductCreatedEvent(id, accountId, supplierProductId));
        }

        public void DelProduct(Guid id, string accountId, int supplierProductId)
        {
            ApplyChange(new SellerProductDelEvent(id, accountId, supplierProductId));
        }
        public void Handle(SellerProductCreatedEvent e)
        {
            this.Id = e.AggregateId;
            this.SupplierProductId = e.SupplierProductId;
            this.AccountId = e.AccountId;
        }
        public void Handle(SellerProductDelEvent e)
        {
            this.Id = e.AggregateId;
            this.SupplierProductId = e.SupplierProductId;
            this.AccountId = e.AccountId;
        }

        public void SetMemenTypeto(SellerProductEntity memento)
        {
            if (memento is SellerProductEntity)
            {
                this.AccountId = memento.AccountId;
                this.SupplierProductId = memento.SupplierProductId.Value;
            }
        }
    }
}
