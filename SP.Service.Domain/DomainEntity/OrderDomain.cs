
using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using SP.Data.Enum;
using SP.Producer;
using SP.Service.Domain.Events;
using SP.Service.Domain.Util;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SP.Service.Domain.DomainEntity
{
    public class OrderDomain : AggregateRoot<Guid>,
        IHandle<OrderCreatedEvent>, IHandle<UpdateShoppingCartOrderIDEvent>,
        IHandle<KafkaAddEvent>, IHandle<ShipOrderEditEvent>, IHandle<ProductSkuEditEvent>,
        IOriginator
    {
        public string OrderId { get; internal set; }
        public string Remark { get; internal set; }
        public OrderStatus OrderStatus { get; internal set; }
        public string CloseReason { get; internal set; }
        public DateTime OrderDate { get; internal set; }
        public DateTime PayDate { get; internal set; }
        public DateTime ShippingDate { get; internal set; }
        public DateTime FinishDate { get; internal set; }
        public string AccountId { get; internal set; }
        public DateTime ShipToDate { get; internal set; }
        public long Freight { get; internal set; }
        public double Amount { get; internal set; }
        public double VIPAmount { get; internal set; }
        public double PurchaseAmount { get; internal set; }
        public string OrderCode { get; internal set; }
        public List<ProductDomain> Products { get; internal set; }
        public int AdressId { get; internal set; }
        public string OrderAddress { get; set; }
        public bool IsVip { get; set; }
        public bool IsWxPay { get; set; }
        public bool IsAliPay { get; set; }
        public int OrderType { get; set; }
        public CouponsFullEntity Coupons { get; set; }

        public OrderDomain()
        {

        }

        public OrderDomain(Guid id, string remark, OrderStatus orderStatus, DateTime orderDate, string accountId, 
            List<ShoppingCartsDomain> shoppingCarts, int addressId,string address,string mobile,bool isvip)
        {
            SumOrderAmount(shoppingCarts,id);
            var mobilePhone = mobile?.Replace("+86","")??string.Empty;
            ApplyChange(new OrderCreatedEvent(id, remark, orderStatus, orderDate, accountId, this.Amount,this.VIPAmount, addressId, address, mobilePhone, isvip));
        }
        public void PurchaseOrderDomain(Guid id, string remark, OrderStatus orderStatus, DateTime orderDate, string accountId,
            List<ShoppingCartsDomain> shoppingCarts, int addressId, string address, string mobile)
        {
            SumPurchaseOrderAmount(shoppingCarts, id);
            var mobilePhone = mobile?.Replace("+86", "") ?? string.Empty;
            ApplyChange(new OrderCreatedEvent(id, remark, orderStatus, orderDate, accountId, this.PurchaseAmount, this.VIPAmount, addressId, address, mobilePhone,false,1));
        }

        private void SumOrderAmount(List<ShoppingCartsDomain> shoppingCarts, Guid orderId)
        {
            this.Amount = 0;
            this.VIPAmount = 0;
            foreach (var item in shoppingCarts)
            {
                if (item.Product != null && item.Product.MarketPrice != null)
                {
                    this.Amount += item.Quantity * item.Product.MarketPrice.Value;                    
                }
                if (item.Product != null && item.Product.VIPPrice != null)
                {
                    this.VIPAmount += item.Quantity * item.Product.VIPPrice.Value;
                }
                ApplyChange(new UpdateShoppingCartOrderIDEvent(orderId,item.CartId, orderId.ToString()));
            }
        }
        private void SumPurchaseOrderAmount(List<ShoppingCartsDomain> shoppingCarts, Guid orderId)
        {
            this.Amount = 0;
            this.VIPAmount = 0;
            foreach (var item in shoppingCarts)
            {
                if (item.Product != null && item.Product.PurchasePrice != null)
                {
                    this.PurchaseAmount += item.Quantity * item.Product.PurchasePrice.Value;
                }
                ApplyChange(new UpdateShoppingCartOrderIDEvent(orderId,item.CartId, orderId.ToString()));
            }
        }

        public void EditOrderDomainStatus(Guid id, OrderStatus orderStatus, OrderPay payWay)
        {
            ApplyChange(new OrderEditEvent(id, orderStatus, payWay));
            
        }
        public void EditShipOrderDomainStatus(Guid id, OrderStatus orderStatus, OrderPay payWay,string accountId)
        {
            ApplyChange(new ShipOrderEditEvent(id, orderStatus, payWay, accountId));
            if (orderStatus == OrderStatus.Success)
            {
                string host = OrderCommon.GetHost();
                ApplyChange(new ProductSkuEditEvent(id, host, accountId));
            }
        }
        public  void AddKafkaInfo(Guid id,OrderStatus orderStatus,int schoolId)
        {
            if (orderStatus == OrderStatus.Payed)
            {                
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                string kafkaIP = string.Empty;
                if (config != null)
                {
                    kafkaIP = config.GetSection("KafkaIP").Value?.ToString()??string.Empty;
                }
                KafkaProducer producer = new KafkaProducer();
                producer.IPConfig = kafkaIP;
                producer.Order = this.GetMemento() as OrdersEntity;
                producer.BuildingId = schoolId;
                ApplyChange(new KafkaAddEvent(id,producer));
            }
        }
        public void AddShipOrderKafka(Guid id,OrderStatus orderStatus, string shippingId, double sumAmount,string orderId,string shipto)
        {
            if (orderStatus == OrderStatus.Payed)
            {
                var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                string kafkaIP = string.Empty;
                if (config != null)
                {
                    kafkaIP = config.GetSection("KafkaIP").Value?.ToString() ?? string.Empty;
                }
                var producer = new SellerProducer();
                producer.IPConfig = kafkaIP;
                producer.ShippingId = shippingId;
                producer.OrderAmount = sumAmount;
                producer.OrderId = orderId;
                producer.Shipto = shipto;
                ApplyChange(new KafkaAddEvent(id,producer));
            }
        }

        public void Handle(OrderCreatedEvent e)
        {
            this.OrderId = e.AggregateId.ToString();
            this.Remark = e.Remark;
            this.OrderStatus = e.OrderStatus;
            this.OrderDate = e.OrderDate;
            this.AccountId = e.AccountId;
            this.AdressId = e.AddressId;
        }

        public void Handle(OrderEditEvent e)
        {
            this.OrderId = e.CommandId;
            this.OrderStatus = e.OrderStatus;
        }

        public void Handle(UpdateShoppingCartOrderIDEvent e)
        {
            
        }
        public void Handle(KafkaAddEvent e)
        {

        }
        public void Handle(ShipOrderEditEvent e)
        {
            this.OrderId = e.CommandId;
            this.OrderStatus = e.OrderStatus;
        }
        public void Handle(ProductSkuEditEvent e)
        {
            this.OrderId = e.CommandId;
        }

        public BaseEntity GetMemento()
        {
            return new OrdersEntity()
            {
                OrderId = this.OrderId,
                OrderDate = this.OrderDate,
                AccountId = this.AccountId,
                OrderStatus = (int)this.OrderStatus,
                UpdateTime = DateTime.Now,
                Remark = this.Remark,
                AddressId = this.AdressId,
                OrderAddress = this.OrderAddress,
                IsVip = this.IsVip,
                IsAliPay = this.IsAliPay,
                IsWxPay = this.IsWxPay,
                OrderCode = this.OrderCode,
                VIPAmount = this.VIPAmount,
                Amount = this.Amount,
                CloseReason = this.CloseReason,
                FinishDate = this.FinishDate,
                PayDate = this.PayDate,
                ShippingDate = this.ShippingDate,
                ShipToDate = this.ShipToDate,
                Freight = this.Freight
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is OrdersEntity)
            {
                var order = memento as OrdersEntity;
                this.OrderId = order.OrderId;
                this.Remark = order.Remark;
                this.OrderStatus = (OrderStatus)order.OrderStatus;
                this.CloseReason = order.CloseReason;
                this.OrderDate = order.OrderDate.Value;
                this.PayDate = order.PayDate != null ? order.PayDate.Value : DateTime.MinValue;
                this.ShippingDate = order.ShippingDate != null ? order.ShippingDate.Value : DateTime.MinValue;
                this.FinishDate = order.FinishDate != null ? order.FinishDate.Value : DateTime.MinValue;
                this.AccountId = order.AccountId;
                this.ShipToDate = order.ShipToDate != null ? order.ShipToDate.Value : DateTime.MinValue;
                this.Freight = order.Freight != null ? order.Freight.Value:0;
                this.Amount = order.Amount.Value;
                this.VIPAmount = order.VIPAmount != null? order.VIPAmount.Value:0;
                this.OrderCode = order.OrderCode!= null? order.OrderCode:string.Empty ;
                this.AdressId = order.AddressId != null ? order.AddressId.Value : 0;
                this.OrderAddress = order.OrderAddress != null ? order.OrderAddress : string.Empty;
                this.IsVip = order.IsVip != null ? order.IsVip.Value : false;
                this.IsWxPay = order.IsWxPay != null ? order.IsWxPay.Value : false;
                this.IsAliPay = order.IsAliPay != null ? order.IsAliPay.Value : false;
                this.OrderType = order.OrderType != null ? order.OrderType.Value : 0;
            }
            else if(memento is ShippingOrderFullEntity)
            {
                var order = memento as ShippingOrderFullEntity;
                this.OrderId = order.OrderId;
                this.AccountId = order.ShipTo;
                this.OrderAddress = order.OrderAddress != null ? order.OrderAddress : string.Empty;
                this.IsVip = order.IsVip != null ? order.IsVip.Value : false;
                this.IsWxPay = order.IsWxPay != null ? order.IsWxPay.Value : false;
                this.IsAliPay = order.IsAliPay != null ? order.IsAliPay.Value : false;
            }
        }

        public void SetMemenProductto(List<ProductDomain> memento)
        {
            if (memento != null)
            {
                this.Products = memento;
            }
        }
    }
}
