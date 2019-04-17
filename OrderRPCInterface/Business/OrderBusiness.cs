using SP.Service;
using SP.Api.Model;
using SP.Api.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;
using SP.Api.Model.Product;
using SP.Api.Model.Account;

namespace OrderGRPCInterface.Business
{
    public class OrderBusiness
    {
        public static string AddMyOrder(OrderModel order,out int status)
        {
            var orderId = string.Empty;
            var client = OrderClientHelper.GetClient();
            var request = new AddOrderRequest()
            {
                AccountId = order.accountId,
                Remark = order.remark != null ? order.remark: string.Empty,
                AddressId = order.addressId,
                OrderType = order.orderType
            };
            order.cartIds.ForEach(x=> request.CartIds.Add(x));
            var result = client.AddMyOrder(request);
            if (result.Status == 10001)
            {
                orderId = result.OrderId;
            }
            if (result.Status == 10004)
            {
                orderId = null;
            }
            status = result.Status;
            return orderId;
        }

        public static List<OrderInfoModel> GetMyOrderList(string accountId, string orderDate)
        {
            var client = OrderClientHelper.GetClient();
            var request1 = new MyOrderRequest()
            {
                AccountId = accountId,
                OrderDate = orderDate
            };
            var result = client.GetMyOrderList(request1);
            var list = new List<OrderInfoModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.OrderList)
                {
                    var domain = new OrderInfoModel();
                    domain.amount = item.Amount;
                    domain.orderId = item.OrderId;
                    domain.orderStatus = item.OrderStatus;
                    domain.orderDate = GetTimestamp(new DateTime(item.OrderDate));
                    if (item.ProductList != null && item.ProductList.Count > 0)
                    {
                        var productList = new List<ProductModel>();
                        foreach (var pitem in item.ProductList)
                        {
                            var p = new ProductModel();
                            p.productId = pitem.ProductId;
                            p.productName = pitem.ProductName;
                            p.productCode = pitem.ProductCode;
                            p.marketPrice = pitem.MarketPrice;
                            p.unit = pitem.Unit;
                            if(pitem.Image.Count > 0)
                            {
                                foreach (var img in pitem.Image)
                                {
                                    var image = new ProductImageModel();
                                    image.id = img.Id;
                                    image.imgPath = img.ImgPath;
                                    image.postion = img.Postion;
                                    p.images.Add(image);
                                }                                
                            }
                            productList.Add(p);
                        }
                        domain.productList = productList;
                    }
                    domain.shopType = item.ShopType;
                    list.Add(domain);
                }
            }
            return list;
        }

        public static List<OrderInfoModel> SearchOrderKeywordList(string accountId,string keyWord,int pageIndex,int pageSize)
        {
            var client = OrderClientHelper.GetClient();
            var request1 = new SearchMyOrderRequest()
            {
                AccountId = accountId,
                KeyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var result = client.SearchOrderKeywordList(request1);
            var list = new List<OrderInfoModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.OrderList)
                {
                    var domain = new OrderInfoModel();
                    domain.amount = item.Amount;
                    domain.orderId = item.OrderId;
                    domain.orderStatus = item.OrderStatus;
                    if (item.ProductList != null && item.ProductList.Count > 0)
                    {
                        foreach (var pitem in item.ProductList)
                        {
                            var p = new ProductModel();
                            p.productId = pitem.ProductId;
                            p.productName = pitem.ProductName;
                            p.productCode = pitem.ProductCode;
                            p.marketPrice = pitem.MarketPrice;
                            p.unit = pitem.Unit;
                            if (pitem.Image.Count > 0)
                            {
                                foreach (var img in pitem.Image)
                                {
                                    var image = new ProductImageModel();
                                    image.id = img.Id;
                                    image.imgPath = img.ImgPath;
                                    image.postion = img.Postion;
                                    p.images.Add(image);
                                }
                            }
                            domain.productList.Add(p);
                        }
                    }
                    list.Add(domain);
                }
            }
            return list;
        }
        public static OrderInfoModel GetOrderByOrderId(string orderId)
        {
            var client = OrderClientHelper.GetClient();
            var request1 = new OrderIdRequest()
            {
                OrderId = orderId
            };
            var result = client.GetOrderByOrderId(request1);
            var list = new List<OrderInfoModel>();
            if (result.Status == 10001)
            {
                var domain = new OrderInfoModel();
                domain.amount = result.OrderInfo.Amount;
                domain.orderId = result.OrderInfo.OrderId;
                domain.orderStatus = result.OrderInfo.OrderStatus;
                domain.orderCode = result.OrderInfo.OrderCode;
                if (result.OrderInfo.ProductList != null && result.OrderInfo.ProductList.Count > 0)
                {
                    var productList = new List<ProductModel>();
                    foreach (var pitem in result.OrderInfo.ProductList)
                    {
                        var p = new ProductModel();
                        p.productId = pitem.ProductId;
                        p.productName = pitem.ProductName;
                        p.productCode = pitem.ProductCode;
                        p.marketPrice = pitem.MarketPrice;
                        p.unit = pitem.Unit;
                        if (pitem.Image.Count > 0)
                        {
                            foreach (var img in pitem.Image)
                            {
                                var image = new ProductImageModel();
                                image.id = img.Id;
                                image.imgPath = img.ImgPath;
                                image.postion = img.Postion;
                                p.images.Add(image);
                            }
                        }
                        productList.Add(p);
                    }
                    domain.productList = productList;
                }
                if (result.OrderInfo.Coupons != null)
                {
                    domain.Coupons = new SP.Api.Model.Order.CouponsModel();
                    domain.Coupons.CouponId = result.OrderInfo.Coupons.CouponId;
                    domain.Coupons.AccountId = result.OrderInfo.Coupons.AccountId;
                    domain.Coupons.Amount = result.OrderInfo.Coupons.Amount;
                    domain.Coupons.AssociatorId = result.OrderInfo.Coupons.AssociatorId;
                    domain.Coupons.Description = result.OrderInfo.Coupons.Description;
                    domain.Coupons.EndDate = new DateTime(result.OrderInfo.Coupons.EndDate);
                    domain.Coupons.KindId = result.OrderInfo.Coupons.KindId;
                    domain.Coupons.ModeDescription = result.OrderInfo.Coupons.ModeDescription;
                    domain.Coupons.ModelAmount = result.OrderInfo.Coupons.ModelAmount;
                    domain.Coupons.StartDate = new DateTime(result.OrderInfo.Coupons.StartDate);
                    domain.Coupons.Status = result.OrderInfo.Coupons.Status;
                }
                return domain;
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateOrderStatus(string orderId,int orderStatus, int payWay = 0)
        {
            bool status = false;
            var client = OrderClientHelper.GetClient();
            var request = new UpdateOrderRequest()
            {
                OrderId= orderId,
                OrderStatus = orderStatus,
                PayWay = payWay
            };
            var result = client.UpdateOrderStatus(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }
        public static bool UpdateShipOrderStatus(string orderId, int orderStatus,string accountId, int payWay = 0)
        {
            bool status = false;
            var client = OrderClientHelper.GetClient();
            var request = new UpdateShipOrderRequest()
            {
                OrderId = orderId,
                OrderStatus = orderStatus,
                PayWay = payWay,
                AccountId = accountId,
            };
            var result = client.UpdateShipOrderStatus(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }
        public static bool UpdateShippingOrder(List<int> shippingOrderId, int orderStatus)
        {
            bool status = false;
            var client = OrderClientHelper.GetClient();
            var request = new UpdateShippingOrderRequest()
            {
                OrderStatus = orderStatus,
            };
            shippingOrderId.ForEach(x=>request.ShipOrderId.Add(x));
            var result = client.UpdateShippingOrder(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }
        public static bool AddCashApply(string accountId,string alipay, double money)
        {
            bool status = false;
            var client = OrderClientHelper.GetClient();
            var request = new AddCashApplyRequest()
            {
                AccountId = accountId,
                Alipay = alipay,
                Money = money
            };
            var result = client.AddCashApply(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }

        public static List<LeadOrderModel> GetSchoolLeadList(string accountId, int orderStatus,int orderType)
        {
            var client = OrderClientHelper.GetClient();
            var request1 = new SchoolLeadRequest()
            {
                AccountId = accountId,
                OrderStatus = orderStatus,
                OrderType = orderType
            };
            var result = client.GetSchoolLeadList(request1);
            var list = new List<LeadOrderModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.OrderInfo)
                {
                    var domain = new LeadOrderModel();
                    domain.amount = item.Amount;
                    domain.orderId = item.OrderId;
                    domain.orderStatus = item.OrderStatus;
                    domain.orderCode = item.OrderCode;
                    domain.orderDate = new DateTime(item.OrderDate).ToString("yyyy-MM-dd");
                    domain.payDate = GetTimestamp(new DateTime(item.PayDate));
                    if(item.Account != null)
                    {
                        domain.account = new SP.Api.Model.Account.AccountModel();
                        domain.account.MobilePhone = item.Account.MobilePhone;
                        domain.account.UserName = item.Account.UserName;
                    }
                    if (item.Address != null)
                    {
                        domain.address = new SP.Api.Model.Account.AddressModel();
                        domain.address.contactAddress = item.Address.ContactAddress;
                        domain.address.id = item.Address.Id;
                        domain.address.gender = (item.Address.Gender == 1);
                        domain.address.dorm = item.Address.DormName;
                        domain.address.districtName = item.Address.DistrictName;
                        domain.address.schoolName = item.Address.SchoolName;
                        domain.address.contactMobile = item.Address.ContactMobile;
                    }
                    if (item.Shop != null)
                    {
                        domain.shop = new ShopModel();
                        domain.shop.shopName = item.Shop.ShopName;
                        domain.shop.startTime = item.Shop.StartTime;
                        domain.shop.endTime = item.Shop.EndTime;
                        domain.shop.shopId = item.Shop.ShopId;
                    }
                    if (item.ShoppingCartList != null && item.ShoppingCartList.Count > 0)
                    {
                        var productList = new List<ShoppingCartModel>();
                        foreach (var pitem in item.ShoppingCartList)
                        {
                            var p = new ShoppingCartModel();
                            p.Product = new ProductModel();
                            p.Product.productName = pitem.ProductName;
                            p.Quantity = pitem.Quantity;
                            p.Amount = pitem.Amount;
                            productList.Add(p);
                        }
                        domain.shoppingCartList = productList;
                    }
                    list.Add(domain);
                }
            }
            return list;
        }
        public static List<TradeModel> GetSchoolLeadTradeList(string accountId, int pageIndex,int pageSize,out long total)
        {
            var client = OrderClientHelper.GetClient();
            var request1 = new TradeRequest()
            {
                AccountId = accountId,
                PageIndex = pageIndex,
                PageSize =pageSize
            };
            var result = client.GetSchoolLeadTradeList(request1);
            var list = new List<TradeModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.TradeList)
                {
                    var domain = new TradeModel();
                    domain.amount = item.Amount;
                    domain.accountId = item.AccountId;
                    domain.cartId = item.CartId;
                    domain.quantity = item.Quantity;
                    if(item.Subject == 1)
                    {
                        domain.title = "增加"+ item.Quantity + "份";
                    }
                    else
                    {
                        domain.title = "提现" + item.Amount + "元";
                    }
                    domain.createTime = new DateTime(item.CreateTime).ToString("yyyy-MM-dd");
                    
                    list.Add(domain);
                }                
            }
            total = result.Total;
            return list;
        }
        public static FinanceModel GetSchoolLeadFinance(string accountId)
        {
            var client = OrderClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId
            };
            var result = client.GetSchoolLeadFinance(request1);
            var model = new FinanceModel();
            if (result.Status == 10001)
            {
                model.haveAmount = result.HaveAmount;
                model.accountId = result.AccountId;
                model.useAmount = result.UseAmount;
                model.activeAmount = result.ActiveAmount;
                model.applyAmount = result.ApplyAmount;
                model.consumeAmount = result.ConsumeAmount;
            }
            return model;
        }

        public static bool UpdateOrderStatusByOrderCode(string orderCode, int orderStatus,int payWay=0)
        {
            bool status = false;
            var client = OrderClientHelper.GetClient();
            var request = new UpdateOrderCodeRequest()
            {
                OrderCode = orderCode,
                OrderStatus = orderStatus,
                PayWay = payWay
            };
            var result = client.UpdateOrderStatusByOrderCode(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }
        
        public static OrderInfoModel GetOrderByOrderCode(string orderCode)
        {
            var client = OrderClientHelper.GetClient();
            var request1 = new OrderCodeRequest()
            {
                OrderCode = orderCode
            };
            var result = client.GetOrderByOrderCode(request1);
            var list = new List<OrderInfoModel>();
            if (result.Status == 10001)
            {
                var domain = new OrderInfoModel();
                domain.amount = result.OrderInfo.Amount;
                domain.orderId = result.OrderInfo.OrderId;
                domain.orderStatus = result.OrderInfo.OrderStatus;
                domain.orderCode = result.OrderInfo.OrderCode;
                if (result.OrderInfo.ProductList != null && result.OrderInfo.ProductList.Count > 0)
                {
                    var productList = new List<ProductModel>();
                    foreach (var pitem in result.OrderInfo.ProductList)
                    {
                        var p = new ProductModel();
                        p.productId = pitem.ProductId;
                        p.productName = pitem.ProductName;
                        p.productCode = pitem.ProductCode;
                        p.marketPrice = pitem.MarketPrice;
                        p.unit = pitem.Unit;
                        if (pitem.Image.Count > 0)
                        {
                            foreach (var img in pitem.Image)
                            {
                                var image = new ProductImageModel();
                                image.id = img.Id;
                                image.imgPath = img.ImgPath;
                                image.postion = img.Postion;
                                p.images.Add(image);
                            }
                        }
                        productList.Add(p);
                    }
                    domain.productList = productList;
                }
                return domain;
            }
            else
            {
                return null;
            }
        }

        public static List<LeadOrderModel> GetShipOrderList(string accountId, int orderStatus, int orderType, int pageIndex, int pageSize)
        {
            var client = OrderClientHelper.GetClient();
            var request1 = new ShipOrderRequest()
            {
                AccountId = accountId,
                OrderStatus = orderStatus,
                OrderType = orderType,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var result = client.GetShipOrderList(request1);
            var list = new List<LeadOrderModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.OrderInfo)
                {
                    var domain = new LeadOrderModel();
                    domain.amount = item.Amount;
                    domain.orderId = item.OrderId;
                    domain.orderStatus = item.OrderStatus;
                    domain.orderCode = item.OrderCode;
                    domain.orderDate = new DateTime(item.OrderDate).ToString("yyyy-MM-dd");
                    domain.payDate = GetTimestamp(new DateTime(item.PayDate));
                    domain.isVip = item.IsVip;
                    if (item.Account != null)
                    {
                        domain.account = new SP.Api.Model.Account.AccountModel();
                        domain.account.MobilePhone = item.Account.MobilePhone;
                        domain.account.UserName = item.Account.UserName;
                    }
                    if (item.Address != null)
                    {
                        domain.address = new SP.Api.Model.Account.AddressModel();
                        domain.address.contactAddress = item.Address.ContactAddress;
                    }
                    
                    if (item.ShoppingCartList != null && item.ShoppingCartList.Count > 0)
                    {
                        var productList = new List<ShoppingCartModel>();
                        foreach (var pitem in item.ShoppingCartList)
                        {
                            var p = new ShoppingCartModel();
                            p.Product = new ProductModel();
                            p.Product.productName = pitem.ProductName;
                            p.Quantity = pitem.Quantity;
                            p.ShipOrderId = pitem.ShipOrderId;
                            productList.Add(p);
                        }
                        domain.shoppingCartList = productList;
                    }
                    list.Add(domain);
                }
            }
            return list;
        }
        public static List<PurchaseOrderBaseModel> GetPurchaseOrderList(string accountId, int pageIndex,int pageSize,out long total)
        {
            List<PurchaseOrderBaseModel> list = new List<PurchaseOrderBaseModel>();
            var client = OrderClientHelper.GetClient();
            var request1 = new PurchaseOrderListRequest()
            {
                AccountId = accountId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var result = client.GetPurchaseOrderList(request1);
            foreach(var item in result.OrderInfo)
            {
                var model = new PurchaseOrderModel();
                model.orderId = item.OrderId;
                if (item.OrderDate > 0)
                {
                    var date = GetTimestamp(new DateTime(item.OrderDate));
                    model.orderDate = date.ToString();
                }
                model.amount = item.Amount;
                model.shopType = item.OrderType;
                list.Add(model);
            }
            total = result.Total;
            return list;
        }
        public static PurchaseOrderModel GetPurchaseOrderByOrderId(string orderId)
        {
            var model = new PurchaseOrderModel();
            var client = OrderClientHelper.GetClient();
            var request1 = new OrderIdRequest()
            {
                OrderId = orderId
            };
            var result = client.GetPurchaseOrderByOrderId(request1);
            model.orderId = result.OrderId;
            model.amount = result.Amount;
            model.orderCode = result.OrderCode;
            if (result.OrderDate > 0)
            {
                var date = GetTimestamp(new DateTime(result.OrderDate));
                model.orderDate = date.ToString();
            }
            if (result.PayDate > 0)
            {
                var date = GetTimestamp(new DateTime(result.PayDate));
                model.orderDate = date.ToString();
            }
            model.orderStatus = result.OrderStatus;
            model.payType = result.PayType;
            model.account = new SP.Api.Model.Account.AccountInfo()
            {
                 AccountId = result.Account.AccountId,
                 FullName = result.Account.UserName
            };
            if (result.Address != null)
            {
                model.address = result.Address.SchoolName+ result.Address.BuildingName+ result.Address.DistrictName+ result.Address.DormName;
            }
            model.shoppingCartList = new List<ShoppingCartModel>();
            if(result.ShoppingCartList != null)
            {
                foreach(var item in result.ShoppingCartList)
                {
                    var cart = new ShoppingCartModel();
                    cart.CartId = item.CartId;
                    cart.Product = new ProductModel();
                    cart.Product.productName = item.ProductName;
                    cart.Product.productId = item.ProductId;
                    cart.Product.marketPrice = item.UnitPrice;
                    cart.ShopType = item.ShopType;
                    model.shoppingCartList.Add(cart);
                }
            }
            
            return model;
        }
        private static long GetTimestamp(DateTime d)
        {
            return (d.ToUniversalTime().Ticks - 621355968000000000) / 10000000;     //精确到毫秒
        }
    }
}
