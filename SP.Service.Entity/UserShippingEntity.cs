using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_UserShipping")]
    public class UserShippingEntity : BaseEntity
    {
        [AutoIncrement]
        public int? ShippingId { get; set; }
        public int AddressId { get; set; }
        public string AccountId { get; set; }
    }
}
