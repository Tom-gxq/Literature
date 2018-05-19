using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_Products")]
    public class ProductEntity : Entity
    {
        [AutoIncrement]
        public override int Id
        {
            get
            {
                return base.Id;
            }

            set
            {
                base.Id = value;
            }
        }
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
        public decimal? MarketPrice { get; set; }
        public decimal? VIPPrice { get; set; }
        public int? BrandId { get; set; }
        public string SuppliersId { get; set; }
        public int? TypeId { get; set; }
        public int? SecondTypeId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long? LastOperater { get; set; }
    }
}
