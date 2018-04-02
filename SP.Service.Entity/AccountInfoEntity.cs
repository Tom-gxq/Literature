using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_AccountInfo")]
    public class AccountInfoEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string AccountId { get; set; }
        public string Avatar { get; set; }
        public string Fullname { get; set; }
        public int? UserType { get; set; }
        public int? Gender { get; set; }
        public string IM_QQ { get; set; }
        public string WeiXin { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
