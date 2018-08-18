using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_AccountProduct")]
    public class AccountProductEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int? ShopId { get; set; }
        public int? PreStock { get; set; }
        public int? Status { get; set; }
    }
}
