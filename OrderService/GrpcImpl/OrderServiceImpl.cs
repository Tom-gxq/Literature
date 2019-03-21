using Grpc.Core;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SP.Service.OrderService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Order.Service.Business;
using SP.Service.Domain;

namespace Order.Service.GrpcImpl
{
    public partial class OrderServiceImpl:OrderServiceBase
    {
        private ILogger logger = new ServiceCollection()
             .AddLogging()
             .BuildServiceProvider()
             .GetService<ILoggerFactory>()
             .AddConsole()
             .CreateLogger("OrderService");

        private int prjLicEID = 7000;

        public OrderServiceImpl(int port)
        {
            if (port > 0)
            {
                this.prjLicEID = port;
            }
        }

        public override Task<AddOrderResponse> AddMyOrder(AddOrderRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "AddMyOrder {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}]  OrderType:[{OrderType}] AddressId:[{AddressId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), 
                request.AccountId ?? string.Empty, request.OrderType, request.AddressId);
            var response = new AddOrderResponse();
            response.Status = 10002;
            try
            {
                response.OrderId = OrderBusiness.AddMyOrder(request);
                response.Status = 10001;
            }
            catch(ProductSkuException skuEx)
            {
                response.Status = 10004;//库存错误
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddMyOrder Exception");
            }
            logger.LogInformation(this.prjLicEID, "AddMyOrder {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<OrderListResponse> GetMyOrderList(MyOrderRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetMyOrderList {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            OrderListResponse response = null;
            try
            {
                response = OrderBusiness.GetMyOrderList(request.AccountId, request.OrderDate);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetMyOrderList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetMyOrderList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<OrderListResponse> SearchOrderKeywordList(SearchMyOrderRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "SearchOrderKeywordList {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            OrderListResponse response = null;
            try
            {
                response = OrderBusiness.SearchOrderKeywordList(request.AccountId,request.KeyWord);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "SearchOrderKeywordList Exception");
            }
            logger.LogInformation(this.prjLicEID, "SearchOrderKeywordList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<OrderResponse> GetOrderByOrderId(OrderIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetOrderByOrderId {Date} {IPAdress} {Status} Connected! OrderId:[{OrderId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.OrderId ?? string.Empty);
            OrderResponse response = null;
            try
            {
                response = OrderBusiness.GetOrderByOrderId(request.OrderId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetOrderByOrderId Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetOrderByOrderId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<OrderStatusResponse> UpdateOrderStatus(UpdateOrderRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "UpdateOrderStatus {Date} {IPAdress} {Status} Connected! OrderId:[{OrderId}] OrderStatus:[{OrderStatus}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.OrderId ?? string.Empty, request.OrderStatus);
            var response = new OrderStatusResponse();
            response.Status = 10002;
            try
            {
                OrderBusiness.UpdateOrderStatus(request.OrderId, request.OrderStatus, request.PayWay);
                response.Status = 10001;
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateOrderStatus Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateOrderStatus {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
               
        public override Task<OrderStatusResponse> UpdateOrderStatusByOrderCode(UpdateOrderCodeRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "UpdateOrderStatusByOrderCode {Date} {IPAdress} {Status} Connected! OrderCode:[{OrderCode}] OrderStatus:[{OrderStatus}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.OrderCode ?? string.Empty, request.OrderStatus);
            var response = new OrderStatusResponse();
            response.Status = 10002;
            try
            {
                OrderBusiness.UpdateOrderStatusByOrderCode(request.OrderCode, request.OrderStatus, request.PayWay);
                response.Status = 10001;
            }
            catch(OrderCodeUpdateException codeEx)
            {
                response.Status = 10003;//code不存在，无效code
                logger.LogError(this.prjLicEID, codeEx, "UpdateOrderStatusByOrderCode OrderCodeUpdateException");
            }
            catch (ProductSkuException skuEx)
            {
                response.Status = 10004;//库存没有了
                logger.LogError(this.prjLicEID, skuEx, "UpdateOrderStatusByOrderCode ProductSkuException");
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateOrderStatusByOrderCode Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateOrderStatusByOrderCode {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<OrderResponse> GetOrderByOrderCode(OrderCodeRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetOrderByOrderCode {Date} {IPAdress} {Status} Connected! OrderCode:[{OrderCode}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.OrderCode ?? string.Empty);
            OrderResponse response = null;
            try
            {
                response = OrderBusiness.GetOrderByOrderCode(request.OrderCode);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetOrderByOrderCode Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetOrderByOrderCode {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        
    }
}
