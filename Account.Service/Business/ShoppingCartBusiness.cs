using SP.Service;
using SP.Service.Domain.Commands.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Service.Business
{
    public class ShoppingCartBusiness
    {
        private static object lockObj = new object();
        private static object lockObj2 = new object();
        public static ShoppingCartResultResponse AddShoppingCart(ShoppingCartRequest request)
        {
            ServiceLocator.CommandBus.Send(new CreatShoppingCartCommand(Guid.NewGuid(), request.ProductId, request.AccountId, request.Quantity, request.ShopId));
            var result = new ShoppingCartResultResponse();
            result.Status = 10001;
            return result;
        }

        public static ShoppingCartListResponse GetMyShoppingCartList(string accountId,int userType)
        {
            var result = new ShoppingCartListResponse();
            result.Status = 10001;
            var list = ServiceLocator.ShppingCartDatabase.GetMyShoppingCartList(accountId);
            
            if (list != null)
            {
                var memberList = ServiceLocator.AssociatorReportDatabase.GetMemberByAccountId(accountId);
                
                foreach (var item in list)
                {
                    var shoppingCart = new ShoppingCart();
                    shoppingCart.AccountId = item.AccountId;
                    shoppingCart.Quantity = item.Quantity;
                    shoppingCart.ShopId = item.ShopId;
                    shoppingCart.CartId = item.CartId;
                    if (item.Product != null)
                    {
                        shoppingCart.ProductId = item.Product.ProductId;
                        shoppingCart.ProductName = item.Product.ProductName;                        
                    }
                    if (memberList.Count > 0 && userType != 3)
                    {
                        shoppingCart.Amount = item.VIPAmount;
                        shoppingCart.UnitPrice = item.Product?.VIPPrice != null ? item.Product.VIPPrice.Value : 0;
                    }
                    else
                    {
                        if (userType == 3)
                        {                            
                            shoppingCart.UnitPrice = item.Product?.PurchasePrice != null ? item.Product.PurchasePrice.Value : 0;
                            shoppingCart.Amount = shoppingCart.UnitPrice*item.Quantity;
                        }
                        else
                        {
                            shoppingCart.Amount = item.Amount;
                            shoppingCart.UnitPrice = item.Product?.MarketPrice != null ? item.Product.MarketPrice.Value : 0;
                        }
                    }
                    result.ShoppingCartList.Add(shoppingCart);
                }
            }
            return result;
        }
        public static ShoppingCartListResponse GetMyShoppingCartListByOrderId(string accountId,string orderId,int userType)
        {
            var result = new ShoppingCartListResponse();
            result.Status = 10001;
            var list = ServiceLocator.ShppingCartDatabase.GetMyShoppingCartListByOrderId(accountId, orderId);

            if (list != null)
            {
                var memberList = ServiceLocator.AssociatorReportDatabase.GetMemberByAccountId(accountId);

                foreach (var item in list)
                {
                    var shoppingCart = new ShoppingCart();
                    shoppingCart.AccountId = item.AccountId;
                    shoppingCart.Quantity = item.Quantity;
                    shoppingCart.ShopId = item.ShopId;
                    shoppingCart.CartId = item.CartId;
                    if (item.Product != null)
                    {
                        shoppingCart.ProductId = item.Product.ProductId;
                        shoppingCart.ProductName = item.Product.ProductName;
                    }
                    if (memberList.Count > 0 && userType != 3)
                    {
                        shoppingCart.Amount = item.VIPAmount;
                        shoppingCart.UnitPrice = item.Product.VIPPrice != null ? item.Product.VIPPrice.Value : 0;
                    }
                    else
                    {
                        if (userType == 3)
                        {
                            shoppingCart.UnitPrice = item.Product?.PurchasePrice != null ? item.Product.PurchasePrice.Value : 0;
                            shoppingCart.Amount = shoppingCart.UnitPrice * item.Quantity;
                        }
                        else
                        {
                            shoppingCart.Amount = item.Amount;
                            shoppingCart.UnitPrice = item.Product.MarketPrice != null ? item.Product.MarketPrice.Value : 0;
                        }
                    }
                    result.ShoppingCartList.Add(shoppingCart);
                }
            }
            return result;
        }

        public static long GetMyShoppingCartCount(string accountId)
        {
            return ServiceLocator.ShppingCartDatabase.GetMyShoppingCartCount(accountId);
        }

        public static bool UpdateShoppingCartQuantity(string cartId, int quantity)
        {
            lock (lockObj)
            {
                lock (lockObj2)
                {
                    return ServiceLocator.ShppingCartDatabase.UpdateShoppingCartQuantity(cartId, quantity);
                }
            }
        }

        public static bool UpdateShoppingCartEnabled(string accountId)
        {
            lock (lockObj)
            {
                return ServiceLocator.ShppingCartDatabase.UpdateShoppingCartEnabled(accountId);
            }
        }

        public static bool UpdateShoppingCartOrderId(string orderId, List<string> list )
        {
            lock (lockObj)
            {
                lock (lockObj2)
                {
                    return ServiceLocator.ShppingCartDatabase.UpdateShoppingCartOrderId(orderId, list);
                }
            }
        }
    }
}
