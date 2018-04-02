using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("AccountAuthentication")]
    public class AuthenticationEntity : BaseEntity
    {
        [AutoIncrement]
        [Alias("AutoId")]
        public int Id { get; set; }
        public int? AuthType { get; set; }
        public string AccountId { get; set; }
        public string Account { get; set; }
        public string VerifyCode { get; set; }
        public string Token { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
