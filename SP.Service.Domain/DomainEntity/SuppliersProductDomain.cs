using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using SP.Service.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class SuppliersProductDomain : AggregateRoot<Guid>, IHandle<SuppliersProductCreatedEvent>,IHandle<SuppliersRegionCreatedEvent>
    {
        public int SuppliersId { get; set; }
        public string ProductId { get; set; }
        public double PurchasePrice { get; set; }
        public int AlertStock { get; set; }
        public int SaleStatus { get; set; }
        public int RegionId { get; set; }

        public SuppliersProductDomain()
        {

        }
        public void Create()
        {
            var @event = new SuppliersProductCreatedEvent(this.Id, this.ProductId, this.SuppliersId, this.PurchasePrice, this.AlertStock);

            ApplyChange(@event);
        }

        public void CreateRegion()
        {
            var @event = new SuppliersRegionCreatedEvent(this.Id, this.SuppliersId, this.RegionId);

            ApplyChange(@event);
        }

        public void Handle(SuppliersProductCreatedEvent e)
        {
            this.Id = e.AggregateId;
            this.ProductId = e.ProductId;
            this.SuppliersId = e.SuppliersId;
            this.AlertStock = e.AlertStock;
            this.PurchasePrice = e.PurchasePrice;
        }
        public void Handle(SuppliersRegionCreatedEvent e)
        {
            this.Id = e.AggregateId;
            this.SuppliersId = e.SuppliersId;
            this.RegionId = e.RegionID;
        }
    }
}
