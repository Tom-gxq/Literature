using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_Associator")]
    public class AssociatorEntity : Entity
    {
        [AutoIncrement]
        public override int Id { get; set; }
        public string AssociatorId { get; set; }
        public string AccountId { get; set; }
        public string KindId { get; set; }
        public int? Quantity { get; set; }
        public double? Amount { get; set; }
        public string PayOrderCode { get; set; }
        public int? PayType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
    }
}
