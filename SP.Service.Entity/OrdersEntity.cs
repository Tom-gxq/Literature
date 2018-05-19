using Grpc.Service.Core.Domain.Entity;
using System;
using ServiceStack.DataAnnotations;

namespace SP.Service.Entity
{
    [Alias("SP_Orders")]
    public class OrdersEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public string OrderId { get; set; }
        public string OrderCode { get; set; }
        public string Remark { get; set; }
        public int? OrderStatus { get; set; }
        public string CloseReason { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string AccountId { get; set; }
        public DateTime? ShipToDate { get; set; }
        public long? Freight { get; set; }
        public double? Amount { get; set; }
        public double? VIPAmount { get; set; }
        public string Meta_Keywords { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? AddressId { get; set; }
        public string OrderAddress { get; set; }
    }
}
