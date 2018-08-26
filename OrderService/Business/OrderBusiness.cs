using Grpc.Service.Core.Dependency;
using SP.Data.Enum;
using SP.Service;
using SP.Service.Domain;
using SP.Service.Domain.Commands;
using SP.Service.Domain.Commands.Order;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Service.Business
{
    public class OrderBusiness
    {
        public static string AddMyOrder(AddOrderRequest request)
        {
            var cartIds = new List<string>();
            var orderId = Guid.NewGuid();
            System.Console.WriteLine("OrderId=" + orderId);
            System.Console.WriteLine("##########################################################################");
            foreach (var item in request.CartIds)
            {
                cartIds.Add(item);
                System.Console.WriteLine("CartIds="+ item);
            }
            System.Console.WriteLine("##########################################################################");

            switch (request.OrderType)
            {
                case 0:
                   ServiceLocator.CommandBus.Send(new CreateOrderCommand(orderId, request.Remark, request.AccountId, cartIds, request.AddressId));
                    break;
                case 1:
                    ServiceLocator.CommandBus.Send(new CreatePurchaseOrderCommand(orderId, request.Remark, request.AccountId, cartIds, request.AddressId,request.OrderType));
                    break;
            }

            
            return orderId.ToString();
        }
        public static OrderListResponse GetMyOrderList(string accountId,string orderDate)
        {
            var result = new OrderListResponse();
            result.Status = 10002;
            DateTime startDate = DateTime.Parse(orderDate).AddDays(-2);
            var maxDate = ServiceLocator.ReportDatabase.GetMyMaxHistoryOrder(accountId);
            if(maxDate < startDate)
            {
                startDate = maxDate.AddDays(-2);
            }
            var list = ServiceLocator.ReportDatabase.GetMyOrderList(accountId, startDate);
            if (list != null)
            {                
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var order = ConvertOrderDomainToResponse(item);
                        result.OrderList.Add(order);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }
        public static SchoolLeadOrderListResponse GetSchoolLeadList(string accountId, int status, int orderType)
        {
            var result = new SchoolLeadOrderListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.GetSchoolLeadList(accountId, status, orderType);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var order = ConvertLeadOrderDomainToResponse(item);
                        result.OrderInfo.Add(order);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }

        public static OrderListResponse SearchOrderKeywordList(string accountId,string keyWord)
        {
            var result = new OrderListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.SearchOrderKeywordList(accountId, keyWord);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var order = ConvertOrderDomainToResponse(item);
                        result.OrderList.Add(order);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }
        public static OrderResponse GetOrderByOrderId(string orderId)
        {
            var result = new OrderResponse();
            result.Status = 10002;
            var order = ServiceLocator.ReportDatabase.GetOrderByOrderId(orderId);
            if (order != null)
            {
                result.OrderInfo = ConvertOrderDomainToResponse(order);
                result.Status = 10001;
            }
            return result;
        }
        public static OrderResponse GetOrderByOrderCode(string orderCode)
        {
            var result = new OrderResponse();
            result.Status = 10002;
            var order = ServiceLocator.ReportDatabase.GetLeadOrderDomainByOrderCode(orderCode);
            if (order != null)
            {
                result.OrderInfo = ConvertOrderDomainToResponse(order);
                result.Status = 10001;
            }
            return result;
        }

        public static void UpdateOrderStatus(string orderId, int orderStatus,int payWay)
        {
            ServiceLocator.CommandBus.Send(new EditOrderCommand(new Guid(orderId), (OrderStatus)orderStatus,(OrderPay)payWay));           
        }
        public static void UpdateShipOrderStatus(string orderId, int orderStatus, int payWay)
        {
            ServiceLocator.CommandBus.Send(new EditPurchaseOrderCommand(new Guid(orderId), (OrderStatus)orderStatus, (OrderPay)payWay));
        }

        public static TradeListResponse GetSchoolLeadTradeList(string accountId, int pageIndex, int pageSize)
        {
            var result = new TradeListResponse();
            result.Status = 10002;
            var tradeList = ServiceLocator.TradeReportDatabase.GetTradeList(accountId, pageIndex, pageSize);
            if (tradeList != null)
            {
                foreach(var item in tradeList)
                {
                    var trade = new SP.Service.Trade();
                    trade.AccountId = item.AccountId;
                    trade.Amount = item.Amount;
                    trade.CartId = item.CartId;
                    trade.CreateTime = item.CreateTime.Ticks;
                    trade.Quantity = item.Quantity;
                    trade.Subject = item.Subject;
                    result.TradeList.Add(trade);
                }
                result.Total = ServiceLocator.TradeReportDatabase.GetTradeListCount(accountId);
                result.Status = 10001;
            }
            return result;
        }
        public static SchoolLeadFinanceResponse GetSchoolLeadFinance(string accountId)
        {
            var result = new SchoolLeadFinanceResponse();
            result.Status = 10002;
            var finance = ServiceLocator.FinanceReportDatabase.GetAccountFinanceDetail(accountId);
            if (finance != null)
            {
                result.AccountId = finance.AccountId;
                result.HaveAmount = finance.HaveAmount;
                result.UseAmount = finance.UseAmount;
                result.ActiveAmount = ServiceLocator.TradeReportDatabase.GetLatelyTrade(accountId);
                result.ApplyAmount = ServiceLocator.CashApplyReportDatabase.GetAllApplyNum(accountId);
                result.Status = 10001;
            }
            return result;
        }

        private static SP.Service.Order ConvertOrderDomainToResponse(OrderDomain entity)
        {
            var memberList = ServiceLocator.AssociatorReportDatabase.GetMemberByAccountId(entity.AccountId);
            var order = new SP.Service.Order();
            order.AccountId = entity.AccountId;
            if (memberList != null && memberList.Count > 0 && entity.OrderType == 0)
            {
                order.Amount = entity.VIPAmount;
            }
            else
            {
                order.Amount = entity.Amount;
            }
            order.CloseReason = entity.CloseReason!= null? entity.CloseReason:string.Empty;
            order.FinishDate = entity.FinishDate > DateTime.MinValue ? entity.FinishDate.Ticks:0;
            order.Freight = entity.Freight;
            order.OrderDate = entity.OrderDate.Ticks;
            order.OrderId = entity.OrderId;
            order.OrderCode = entity.OrderCode;
            order.OrderStatus = (int)entity.OrderStatus;
            order.PayDate = entity.PayDate > DateTime.MinValue ? entity.PayDate.Ticks : 0;
            order.Remark = entity.Remark != null ? entity.Remark : string.Empty;
            order.ShippingDate = entity.ShippingDate > DateTime.MinValue ? entity.ShippingDate.Ticks : 0;
            order.ShipToDate = entity.ShipToDate > DateTime.MinValue ? entity.ShipToDate.Ticks : 0;
            if(entity.Products!=null)
            {
                foreach (var item in entity.Products)
                {
                    if (item != null && !string.IsNullOrEmpty(item.ProductId))
                    {
                        var product = ConvertProductEntityToResponse(item);
                        order.ProductList.Add(product);
                    }
                }
            }
            return order;
        }

        private static SP.Service.Product ConvertProductEntityToResponse(ProductDomain entity)
        {
            var product = new SP.Service.Product();
            product.ProductId = entity.ProductId;
            product.ProductName = !string.IsNullOrEmpty(entity.ProductName) ? entity.ProductName : string.Empty;
            product.ProductCode = !string.IsNullOrEmpty(entity.ProductCode) ? entity.ProductCode : string.Empty;
            product.SaleStatus = entity.SaleStatus;
            product.ShortDescription = !string.IsNullOrEmpty(entity.ShortDescription) ? entity.ShortDescription : string.Empty;
            product.Unit = !string.IsNullOrEmpty(entity.Unit) ? entity.Unit : string.Empty;
            product.AddedDate = entity.AddedDate.Ticks;
            product.Description = !string.IsNullOrEmpty(entity.Description)? entity.Description : string.Empty;
            product.MarketPrice = entity.MarketPrice;
            product.VipPrice = entity.VipPrice;
            return product;
        }
        private static SchoolLeadOrder ConvertLeadOrderDomainToResponse(LeadOrderDomain entity)
        {
            var order = new SchoolLeadOrder();
            order.Amount = entity.Amount;
            order.OrderDate = entity.OrderDate.Ticks;
            order.OrderId = entity.OrderId;
            order.OrderCode = entity.OrderCode;
            order.PayDate = entity.PayDate > DateTime.MinValue ? entity.PayDate.Ticks : 0;
            order.OrderStatus = (int)entity.OrderStatus;
            if(entity.Account != null)
            {
                order.Account = new SP.Service.AccountInfo();
                order.Account.MobilePhone = entity.Account.MobilePhone;
                order.Account.UserName = !string.IsNullOrEmpty(entity.Account.UserName)? entity.Account.UserName:string.Empty;
            }
            if(entity.Address != null)
            {
                var addressArray = entity.OrderAddress?.Split(" ");
                order.Address = new SP.Service.Address();
                order.Address.ContactAddress = entity.OrderAddress??string.Empty;
                order.Address.Id  = 0;
                order.Address.Gender = 1;
                if (addressArray != null && addressArray.Length > 2)
                {
                    order.Address.Dorm = $"{addressArray[2]} {addressArray[3]}" ?? string.Empty;
                    order.Address.DistrictName = addressArray[1] ?? string.Empty;
                    order.Address.SchoolName = addressArray[0] ?? string.Empty;
                }
                else
                {
                    order.Address.Dorm = string.Empty;
                    order.Address.DistrictName = string.Empty;
                    order.Address.SchoolName = string.Empty;
                }
                order.Address.ContactMobile =  string.Empty;
                order.Address.ContactName =  string.Empty;
            }
            if (entity.Shop != null)
            {
                order.Shop = new Shop();
                order.Shop.ShopName = entity.Shop.ShopName??string.Empty;
                order.Shop.StartTime = !string.IsNullOrEmpty(entity.Shop.StartTime)? entity.Shop.StartTime:string.Empty;
                order.Shop.EndTime = !string.IsNullOrEmpty(entity.Shop.EndTime) ? entity.Shop.EndTime : string.Empty;
                order.Shop.ShopId = entity.Shop.Id;
            }
            if (entity.ShoppingCarts != null)
            {
                var memberList = ServiceLocator.AssociatorReportDatabase.GetMemberByAccountId(entity.AccountId);
                foreach (var item in entity.ShoppingCarts)
                {
                    var shoppingCart = new ShoppingCart();
                    shoppingCart.ProductName = item.Product.ProductName;
                    shoppingCart.Quantity = item.Quantity;
                    shoppingCart.UnitPrice = item.Product?.MarketPrice != null? item.Product.MarketPrice.Value:0;
                    if (memberList.Count > 0)
                    {
                        shoppingCart.Amount = item.VIPAmount;
                    }
                    else
                    {
                        shoppingCart.Amount = item.Amount;
                    }
                    order.ShoppingCartList.Add(shoppingCart);
                }
            }
            return order;
        }
        public static void AddCashApply(string accountId, string alipay, double money)
        {
            ServiceLocator.CommandBus.Send(new CreateCashApplyCommand(accountId, alipay, money));
        }

        public static void  UpdateOrderStatusByOrderCode(string orderCode, int orderStatus, int payWay)
        {
            ServiceLocator.CommandBus.Send(new EditOrderCodeCommand(orderCode, (OrderStatus)orderStatus, (OrderPay)payWay));
        }

    }
}
