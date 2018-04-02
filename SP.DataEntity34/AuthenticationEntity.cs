using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("AccountAuthentication")]
    public class AuthenticationEntity : Entity
    {
        [AutoIncrement]
        [Alias("AutoId")]
        public override int Id { get; set; }
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
