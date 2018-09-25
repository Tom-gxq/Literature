using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Application.Order.DTO;
using LibMain.Domain.Repositories;
using Lib.Application.Services;
using SP.DataEntity;
using LibMain.Dependency;
using SP.ManageEntityFramework.Repositories;

namespace SP.Application.Order
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<OrdersEntity, int> _orderRepository;
        public OrderAppService(IRepository<OrdersEntity, int> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<OrderDto> GetOrderList(int status,int pageIndex,int pageSize)
        {
            var retList = new List<OrderDto>();
            var repository = IocManager.Instance.Resolve<OrdersRespository>();
            var list = repository.GetOrderList(status, pageIndex, pageSize);
            foreach(var item in list)
            {
                var order = ConvertFromRepositoryEntity(item);
                retList.Add(order);
            }
            return retList;
        }
        public long GetOrderListCount(int status)
        {
            var repository = IocManager.Instance.Resolve<OrdersRespository>();
            var count = repository.GetOrderListCount(status);
            return count;
        }

        public bool UpdateOrderStatus(string orderId, int status)
        {
            var result = _orderRepository.UpdateNonDefaults(new OrdersEntity()
            {
                OrderId = orderId,
                OrderStatus = status
            },x=>x.OrderId == orderId);
            return result > 0;
        }
        public bool DeleteOrderById(string orderId)
        {
            var retuslt = false;
            try
            {
                _orderRepository.Delete(x => x.OrderId == orderId);
                retuslt = true;
            }
            catch
            {

            }
            return retuslt;
        }

        public List<OrderDto> SearchOrderListByKeyWord(string keyWord, int pageIndex, int pageSize)
        {
            var retList = new List<OrderDto>();
            var repository = IocManager.Instance.Resolve<OrdersRespository>();
            var list = repository.SearchOrderListByKeyWord(keyWord, pageIndex, pageSize);
            foreach (var item in list)
            {
                var order = ConvertFromRepositoryEntity(item);
                retList.Add(order);
            }
            return retList;
        }

        public long SearchOrderListByKeyWordCount(string keyWord)
        {
            var repository = IocManager.Instance.Resolve<OrdersRespository>();
            var count = repository.SearchOrderListByKeyWordCount(keyWord);
            return count;
        }


        private static OrderDto ConvertFromRepositoryEntity(OrdersEntity order)
        {
            if (order == null)
            {
                return null;
            }
            var orderDto = new OrderDto
            {
                AccountId = order.AccountId,
                Amount = order.Amount == null ? 0 : order.Amount.Value,
                CloseReason = order.CloseReason,
                FinishDate = order.FinishDate == null ? DateTime.MinValue : order.FinishDate.Value,
                Freight = order.Freight == null ? 0 : order.Freight.Value,
                OrderDate = order.OrderDate.Value,
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus.Value,
                PayDate = order.PayDate == null ? DateTime.MinValue : order.PayDate.Value,
                Remark = order.Remark ?? string.Empty,
                ShippingDate = order.ShippingDate == null ? DateTime.MinValue : order.ShippingDate.Value,
                ShipToDate = order.ShipToDate == null ? DateTime.MinValue : order.ShipToDate.Value,
                OrderCode = order.OrderCode,
                OrderAddress = order.OrderAddress ?? string.Empty,
                IsAliPay = order.IsAliPay != null ? order.IsAliPay.Value : false,
                IsVip = order.IsVip != null ? order.IsVip.Value : false,
                IsWxPay = order.IsWxPay != null ? order.IsWxPay.Value : false,
                Shiper = new List<User.DTO.AccountInfoDto>(),
                Owner = new User.DTO.AccountInfoDto()
                {
                    Fullname = string.Empty,
                    Mobile = string.Empty
                }
            };
            orderDto.Amount = orderDto.IsVip ? (order.VIPAmount == null ? 0 : order.VIPAmount.Value) : (order.Amount == null ? 0 : order.Amount.Value);
            var repository = IocManager.Instance.Resolve<AccountInfoRepository>();
            var accountRep = IocManager.Instance.Resolve<AccountRespository>();
            if (!string.IsNullOrEmpty(order.AccountId))
            {
                var accountInfo = repository.GetAccountInfoById(order.AccountId);
                orderDto.Owner.Fullname = accountInfo?.Fullname ?? string.Empty;                
                var account = accountRep.GetAccountById(order.AccountId);
                orderDto.Owner.Mobile = account?.MobilePhone?.Replace("+86", "") ?? string.Empty;
            }

            var orderRep = IocManager.Instance.Resolve<ShipOrderRespository>();
            var list = orderRep.GetOrderShippingByOrderId(orderDto.OrderId);
            if (list != null && list.Count>0)
            {
                var group = list.GroupBy(x=>x.ShippingId);
                foreach (var item in group)
                {
                    if (item != null && !string.IsNullOrEmpty(item.Key)
                        && (orderDto.Shiper.Find(x=>x.AccountId == item.Key) == null))
                    {
                        var shiper = new User.DTO.AccountInfoDto();
                        shiper.AccountId = item.Key;
                        var shiperInfo = repository.GetAccountInfoById(item.Key);
                        shiper.Fullname = shiperInfo?.Fullname??string.Empty;                        
                        var shiperAccount = accountRep.GetAccountById(order.AccountId);                        
                        shiper.Mobile = shiperAccount?.MobilePhone?.Replace("+86", "") ?? string.Empty;
                        orderDto.Shiper.Add(shiper);
                    }
                }
            }            
            return orderDto;
        }        
    }
}
