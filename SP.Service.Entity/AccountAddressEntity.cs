using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_AccountAddress")]
    public class AccountAddressEntity : BaseEntity
    {
        [AutoIncrement]
        [Alias("Id")]
        public int? ID { get; set; }
        public string UserName { get; set; }
        public int? Gender { get; set; }
        public string Mobile { get; set; }
        public int? RegionID { get; set; }
        public string Address { get; set; }
        public string AccountId { get; set; }
        public int? IsDefault { get; set; }
        public string Dorm { get; set; }
    }
}
