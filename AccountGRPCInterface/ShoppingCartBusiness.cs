using SP.Api.Model.Account;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountGRPCInterface
{
    public class ShoppingCartBusiness
    {
        public static bool AddShoppingCart(ShoppingCartModel model)
        {
            string reuslt = string.Empty;
            var client = AccountClientHelper.GetClient();
            var request1 = new ShoppingCartRequest()
            {
                AccountId = model.AccountId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                ShopId = model.ShopId
            };
            var result = client.AddShoppingCart(request1);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<ShoppingCartModel> GetMyShoppingCartList(string accountId)
        {
            string reuslt = string.Empty;
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId,
            };
            var result = client.GetMyShoppingCartList(request1);
            var list = new List<ShoppingCartModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.ShoppingCartList)
                {
                    var domain = new ShoppingCartModel();
                    domain.CartId = item.CartId;
                    domain.ProductId = item.ProductId;
                    domain.ShopId = item.ShopId;
                    domain.Quantity = item.Quantity;
                    domain.Product = new SP.Api.Model.Product.ProductModel()
                    {
                        productId = item.ProductId,
                        productName = item.ProductName,
                        marketPrice = item.UnitPrice
                    };
                    domain.CreateTime = new DateTime(item.CreateTime);
                    domain.Amount = item.Amount;
                    list.Add(domain);
                }
            }
            return list;
        }
        

        public static long GetMyShoppingCartCount(string accountId)
        {
            string reuslt = string.Empty;
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId,
            };
            var result = client.GetMyShoppingCartCount(request1);
            
            if (result.Status == 10001)
            {
                return result.Count;
            }
            else
            {
                return 0;
            }
        }

        public static bool UpdateShoppingCartQuantity(string  cartId,int quantity)
        {
            string reuslt = string.Empty;
            var client = AccountClientHelper.GetClient();
            var request1 = new ShoppingCartQuantityRequest()
            {                
                CartId = cartId,
                Quantity = quantity,
            };
            var result = client.UpdateShoppingCartQuantity(request1);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateShoppingCartEnabled(string accountId)
        {
            string reuslt = string.Empty;
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId
            };
            var result = client.UpdateShoppingCartEnabled(request1);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateShoppingCartOrderId(string orderId,List<string> cartIdList)
        {
            string reuslt = string.Empty;
            var client = AccountClientHelper.GetClient();
            var request1 = new ShoppingCartOrderIdRequest()
            {
                OrderId = orderId,                
            };
            if(cartIdList != null)
            {
                cartIdList.ForEach(x => request1.CartId.Add(x));
            }
            var result = client.UpdateShoppingCartOrderId(request1);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static PreOrderModel GetMyPreOrderList(string accountId)
        {
            string reuslt = string.Empty;
            var model = new PreOrderModel();
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId,
            };
            var result = client.GetMyShoppingCartList(request1);
            var list = new List<ShoppingCartModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.ShoppingCartList)
                {
                    var domain = new ShoppingCartModel();
                    domain.CartId = item.CartId;
                    domain.ProductId = item.ProductId;
                    domain.ShopId = item.ShopId;
                    domain.Quantity = item.Quantity;
                    domain.Product = new SP.Api.Model.Product.ProductModel()
                    {
                        productId = item.ProductId,
                        productName = item.ProductName,
                        marketPrice = item.UnitPrice
                    };
                    domain.CreateTime = new DateTime(item.CreateTime);
                    domain.Amount = item.Amount;
                    list.Add(domain);
                }
            }
            model.shoppingCartList = list;

            double amount = 0;
            list.ForEach(x => amount += x.Amount);
            model.amoutTotal = amount;

            var addressResult = client.GetDefaultSelectedAddress(request1);
            var addressDomain = new AddressModel();
            if (result.Status == 10001)
            {
                addressDomain.id = addressResult.Address.Id;
                addressDomain.districtId = addressResult.Address.DistrictId;
                addressDomain.districtName = addressResult.Address.DistrictName;
                addressDomain.schoolId = addressResult.Address.SchoolId;
                addressDomain.schoolName = addressResult.Address.SchoolName;
                addressDomain.status = addressResult.Address.Status;
                addressDomain.gender = addressResult.Address.Gender == 1 ? true : false;
                addressDomain.contactAddress = addressResult.Address.ContactAddress;
                addressDomain.contactMobile = addressResult.Address.ContactMobile;
                addressDomain.contactName = addressResult.Address.ContactName;
                addressDomain.dorm = addressResult.Address.Dorm;
            }
            model.defaultAddress = addressDomain;
            return model;
        }

        public static PreOrderModel GetMyShoppingCartListByOrderId(string accountId, string orderId)
        {
            string reuslt = string.Empty;
            var model = new PreOrderModel();
            var client = AccountClientHelper.GetClient();
            var request1 = new MyShoppingCartRequest()
            {
                AccountId = accountId,
                OrderId = orderId
            };
            var result = client.GetMyShoppingCartListByOrderId(request1);
            var list = new List<ShoppingCartModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.ShoppingCartList)
                {
                    var domain = new ShoppingCartModel();
                    domain.CartId = item.CartId;
                    domain.ProductId = item.ProductId;
                    domain.ShopId = item.ShopId;
                    domain.Quantity = item.Quantity;
                    domain.Product = new SP.Api.Model.Product.ProductModel()
                    {
                        productId = item.ProductId,
                        productName = item.ProductName,
                        marketPrice = item.UnitPrice
                    };
                    domain.CreateTime = new DateTime(item.CreateTime);
                    domain.Amount = item.Amount;
                    list.Add(domain);
                }
            }
            model.shoppingCartList = list;

            double amount = 0;
            list.ForEach(x => amount += x.Amount);
            model.amoutTotal = amount;

            var request2 = new AccountIdRequest()
            {
                AccountId = accountId,
            };
            var addressResult = client.GetDefaultSelectedAddress(request2);
            var addressDomain = new AddressModel();
            if (result.Status == 10001)
            {
                addressDomain.id = addressResult.Address.Id;
                addressDomain.districtId = addressResult.Address.DistrictId;
                addressDomain.districtName = addressResult.Address.DistrictName;
                addressDomain.schoolId = addressResult.Address.SchoolId;
                addressDomain.schoolName = addressResult.Address.SchoolName;
                addressDomain.status = addressResult.Address.Status;
                addressDomain.gender = addressResult.Address.Gender == 1 ? true : false;
                addressDomain.contactAddress = addressResult.Address.ContactAddress;
                addressDomain.contactMobile = addressResult.Address.ContactMobile;
                addressDomain.contactName = addressResult.Address.ContactName;
                addressDomain.dorm = addressResult.Address.Dorm;
            }
            model.defaultAddress = addressDomain;
            return model;
        }
    }
}
