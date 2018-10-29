using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsCreateEvent : Event
    {
        public int Num_NewUser { get; internal set; }
        public int Num_NewAssociator { get; internal set; }
        public int Num_BuyAssociator { get; internal set; }
        public int Num_NewOrder { get; internal set; }
        public double Num_FoodOrderAmount { get; internal set; }
        public double Num_MarkOrderAmount { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public SysStatisticsCreateEvent(DateTime createTime,int num_NewUser, int num_NewAssociator, int num_BuyAssociator,
            int num_NewOrder, double foodAmount, double markAmount)
        {
            this.CreateTime = createTime;
            this.Num_BuyAssociator = num_BuyAssociator;
            this.Num_NewAssociator = num_NewAssociator;
            this.Num_NewOrder = num_NewOrder;
            this.Num_NewUser = num_NewUser;
            this.Num_FoodOrderAmount = foodAmount;
            this.Num_MarkOrderAmount = markAmount;
        }
    }
}
