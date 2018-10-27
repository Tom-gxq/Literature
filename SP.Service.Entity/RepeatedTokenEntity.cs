using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_RepeatedToken")]
    public class RepeatedTokenEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string AccessToken { get; set; }
        public string AccountID { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
