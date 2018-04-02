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
                Remark = order.Remark,
                ShippingDate = order.ShippingDate == null ? DateTime.MinValue :order.ShippingDate.Value,
                ShipToDate = order.ShipToDate == null ? DateTime.MinValue : order.ShipToDate.Value,
                OrderCode = order.OrderCode
            };

            return orderDto;
        }        
    }
}
