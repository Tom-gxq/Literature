using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsCreateEvent : Event
    {
        public int Num_NewUser { get; set; }
        public int Num_NewAssociator { get; set; }
        public int Num_BuyAssociator { get; set; }
        public int Num_NewOrder { get; set; }
        public double Num_OrderAmount { get; set; }
        public DateTime CreateTime { get; set; }
        public SysStatisticsCreateEvent(DateTime createTime,int num_NewUser, int num_NewAssociator, int num_BuyAssociator,
            int num_NewOrder, double num_OrderAmount)
             : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CreateTime = createTime;
            this.Num_BuyAssociator = num_BuyAssociator;
            this.Num_NewAssociator = num_NewAssociator;
            this.Num_NewOrder = num_NewOrder;
            this.Num_NewUser = num_NewUser;
            this.Num_OrderAmount = num_OrderAmount;
            this.EventType = EventType.SysStatisticsCreate;
        }
    }
}
