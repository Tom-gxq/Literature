using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccessTokenCreatedEvent : Event
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public DateTime AccessTokenExpires { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public DateTime CreateTime { get; set; }
        public AccessTokenCreatedEvent(Guid id,string accountId, string accessToken, DateTime accessTokenExpires,
            string refreshToken, DateTime refreshTokenExpires, DateTime createTime)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            CommandId = id.ToString();
            AccessToken = accessToken;
            AccountId = accountId;
            AccessTokenExpires = accessTokenExpires;
            RefreshToken = refreshToken;
            RefreshTokenExpires = refreshTokenExpires;
            CreateTime = createTime;
            EventType = EventType.AccessTokenCreated;
        }
    }
}
