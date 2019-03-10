using Grpc.Service.Core.Domain.Entity;
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
    }
}
