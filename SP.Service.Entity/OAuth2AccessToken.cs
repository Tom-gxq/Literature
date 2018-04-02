using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("OAuth2_Access_Token")]
    public class OAuth2AccessToken : BaseEntity
    {
        [AutoIncrement]
        [Alias("AutoID")]
        public int? ID { get; set; }
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public DateTime? AccessTokenExpires { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
