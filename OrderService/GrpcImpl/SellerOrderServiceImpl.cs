using Grpc.Core;
using Microsoft.Extensions.Logging;
using Order.Service.Business;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SP.Service.OrderService;

namespace Order.Service.GrpcImpl
{
    public partial class OrderServiceImpl : OrderServiceBase
    {

        public override Task<SchoolLeadOrderListResponse> GetShipOrderList(ShipOrderRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetShipOrderList {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}] OrderStatus:[{OrderStatus}]  OrderType:[{OrderType}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(),
                request.AccountId ?? string.Empty, request.OrderStatus, request.OrderType);
            SchoolLeadOrderListResponse response = null;
            try
            {
                response = OrderBusiness.GetShipOrderList(request.AccountId, request.OrderStatus, request.OrderType, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetShipOrderList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetShipOrderList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<OrderStatusResponse> UpdateShippingOrder(UpdateShippingOrderRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "UpdateShippingOrder {Date} {IPAdress} {Status} Connected! OrderStatus:[{OrderStatus}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.OrderStatus);
            foreach (var item in request.ShipOrderId)
            {
                System.Console.WriteLine($"UpdateShippingOrder ShipOrderId = {item} {DateTime.Now.ToLongDateString()}");
            }
            var response = new OrderStatusResponse();
            response.Status = 10002;
            try
            {
                OrderBusiness.UpdateShippingOrder(request.ShipOrderId.ToList(), request.OrderStatus);
                response.Status = 10001;
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateShippingOrder Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateShippingOrder {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<OrderStatusResponse> UpdateShipOrderStatus(UpdateShipOrderRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "UpdateShipOrderStatus {Date} {IPAdress} {Status} Connected! OrderId:[{OrderId}] OrderStatus:[{OrderStatus}] AccountId:[{AccountId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.OrderId ?? string.Empty, request.OrderStatus, request.AccountId);
            var response = new OrderStatusResponse();
            response.Status = 10002;
            try
            {
                OrderBusiness.UpdateShipOrderStatus(request.OrderId, request.OrderStatus, request.PayWay, request.AccountId);
                response.Status = 10001;
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateShipOrderStatus Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateShipOrderStatus {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SchoolLeadFinanceResponse> GetSchoolLeadFinance(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSchoolLeadFinance {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            SchoolLeadFinanceResponse response = null;
            try
            {
                response = OrderBusiness.GetSchoolLeadFinance(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSchoolLeadFinance Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSchoolLeadFinance {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<OrderStatusResponse> AddCashApply(AddCashApplyRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "AddCashApply {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}] ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            var response = new OrderStatusResponse();
            response.Status = 10002;
            try
            {
                OrderBusiness.AddCashApply(request.AccountId, request.Alipay, request.Money);
                response.Status = 10001;
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddCashApply Exception");
            }
            logger.LogInformation(this.prjLicEID, "AddCashApply {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<TradeListResponse> GetSchoolLeadTradeList(TradeRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSchoolLeadTradeList {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            TradeListResponse response = null;
            try
            {
                response = OrderBusiness.GetSchoolLeadTradeList(request.AccountId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSchoolLeadTradeList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSchoolLeadTradeList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SchoolLeadOrderListResponse> GetSchoolLeadList(SchoolLeadRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSchoolLeadList {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}] OrderStatus:[{OrderStatus}]  OrderType:[{OrderType}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(),
                request.AccountId ?? string.Empty, request.OrderStatus, request.OrderType);
            SchoolLeadOrderListResponse response = null;
            try
            {
                response = OrderBusiness.GetSchoolLeadList(request.AccountId, request.OrderStatus, request.OrderType);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSchoolLeadList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSchoolLeadList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<PurchaseOrderListResponse> GetPurchaseOrderList(PurchaseOrderListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetPurchaseOrderList {Date} {IPAdress} {Status} Connected! AccountId:[{AccountId}] PageIndex:[{PageIndex}]  PageSize:[{PageSize}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(),
                request.AccountId ?? string.Empty, request.PageIndex, request.PageSize);
            PurchaseOrderListResponse response = null;
            try
            {
                response = OrderBusiness.GetPurchaseOrderList(request.AccountId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetPurchaseOrderList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetPurchaseOrderList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<PurchaseOrderResponse> GetPurchaseOrderByOrderId(OrderIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetPurchaseOrderByOrderId {Date} {IPAdress} {Status} Connected! OrderId:[{OrderId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(),request.OrderId );
            PurchaseOrderResponse response = null;
            try
            {
                response = OrderBusiness.GetPurchaseOrderByOrderId(request.OrderId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetPurchaseOrderByOrderId Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetPurchaseOrderByOrderId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
    }
}
