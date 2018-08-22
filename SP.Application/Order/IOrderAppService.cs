using Lib.Application.Services;
using SP.Application.Order.DTO;
using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Order
{
    public interface IOrderAppService : IApplicationService
    {
        List<OrderDto> GetOrderList(int status, int pageIndex, int pageSize);
        long GetOrderListCount(int status);
        bool UpdateOrderStatus(string orderId, int status);
        bool DeleteOrderById(string orderId);
        List<OrderDto> SearchOrderListByKeyWord(string keyWord, int pageIndex, int pageSize);
        long SearchOrderListByKeyWordCount(string keyWord);
    }
}
