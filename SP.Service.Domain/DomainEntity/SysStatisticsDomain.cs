using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class SysStatisticsDomain : AggregateRoot<Guid>, IHandle<SysStatisticsSumOrderEvent>,
        IHandle<SysStatisticsCreateEvent>, IHandle<SysStatisticsSumBuyMemberEvent>
        , IHandle<SysStatisticsSumNewMemberEvent>, IHandle<SysStatisticsSumUserEvent>
    {
        public int Num_NewUser { get; set; }
        public int Num_NewAssociator { get; set; }
        public int Num_BuyAssociator { get; set; }
        public int Num_NewOrder { get; set; }
        public double Num_FoodOrderAmount { get; set; }
        public double Num_MarkOrderAmount { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreateTime { get; set; }
        public SysStatisticsDomain()
        {

        }

        public SysStatisticsDomain(Guid id,DateTime createTime, int num_NewUser, int num_NewAssociator, int num_BuyAssociator,
            int num_NewOrder, double foodAmount, double markAmount)
        {
            ApplyChange(new SysStatisticsCreateEvent(id,createTime, num_NewUser, num_NewAssociator, num_BuyAssociator, num_NewOrder, foodAmount, markAmount));
        }

        public void SumOrderStatistics(Guid id,string orderId, string orderCode, DateTime orderDate, string accountId,
            double foodAmount, double markAmount, int addressId)
        {
            ApplyChange(new SysStatisticsSumOrderEvent(id,orderId, orderCode, accountId, foodAmount, markAmount, addressId, orderDate));
        }

        public void SumBuyMemberStatistics(Guid id,string accountId, double amount, DateTime createTime)
        {
            ApplyChange(new SysStatisticsSumBuyMemberEvent(id,accountId, amount, createTime));
        }
        public void SumNewMemberStatistics(Guid id,string accountId, double amount, DateTime createTime)
        {
            ApplyChange(new SysStatisticsSumNewMemberEvent(id,accountId, amount, createTime));
        }

        public void SumUserStatistics(string accountId, DateTime createTime)
        {
            ApplyChange(new SysStatisticsSumUserEvent(new Guid(accountId),accountId, createTime));
        }

        public void Handle(SysStatisticsSumOrderEvent e)
        {
            
        }

        public void Handle(SysStatisticsCreateEvent e)
        {

        }

        public void Handle(SysStatisticsSumBuyMemberEvent e)
        {

        }

        public void Handle(SysStatisticsSumNewMemberEvent e)
        {

        }
        public void Handle(SysStatisticsSumUserEvent e)
        {

        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is SysStatisticsEntity)
            {
                var order = memento as SysStatisticsEntity;
                this.Num_NewUser = order.Num_NewUser!= null? order.Num_NewUser.Value:0;
                this.CreateTime = order.CreateTime.Value;
                this.Num_NewAssociator = order.Num_NewAssociator != null ? order.Num_NewAssociator.Value : 0;
                this.Num_BuyAssociator = order.Num_BuyAssociator != null ? order.Num_BuyAssociator.Value : 0;
                this.Num_NewOrder = order.Num_NewOrder != null ? order.Num_NewOrder.Value : 0;
                this.Num_FoodOrderAmount = order.Num_FoodOrderAmount != null ? order.Num_FoodOrderAmount.Value : 0;
                this.Num_MarkOrderAmount = order.Num_MarkOrderAmount != null ? order.Num_MarkOrderAmount.Value : 0;
                this.IsChecked = order.IsChecked != null ? order.IsChecked.Value : false;
            }
        }
    }
}
