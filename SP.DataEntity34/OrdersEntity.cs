using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_Orders")]
    public class OrdersEntity : Entity
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

        public string OrderId { get; set; }
        public string Remark { get; set; }
        public int? OrderStatus { get; set; }
        public string CloseReason { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string AccountId { get; set; }
        public DateTime? ShipToDate { get; set; }
        public decimal? Freight { get; set; }
        public decimal? Amount { get; set; }
        public decimal? VIPAmount { get; set; }
        public string Meta_Keywords { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string OrderCode { get; set; }
        public string OrderAddress { get; set; }
        public bool? IsVip { get; set; }
        public bool? IsWxPay { get; set; }
        public bool? IsAliPay { get; set; }
    }
}
