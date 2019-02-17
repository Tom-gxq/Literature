using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_AccountOpenId")]
    public class AccountOpenIdEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string AccountId { get; set; }
        public string WxOpenId { get; set; }
        public int? WxType { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
