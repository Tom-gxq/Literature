using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_Products")]
    public class ProductEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ShortDescription { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public string Meta_Keywords { get; set; }
        public int? SaleStatus { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? DisplaySequence { get; set; }
        public double? MarketPrice { get; set; }
        public double? VIPPrice { get; set; }
        public int? BrandId { get; set; }
        public int SuppliersId { get; set; }
        public long? TypeId { get; set; }
        public long? SecondTypeId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long? LastOperater { get; set; }
        public double? PurchasePrice { get; set; }
    }
}
