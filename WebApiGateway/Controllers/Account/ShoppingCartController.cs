using AccountGRPCInterface;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Account
{
    public class ShoppingCartController : BaseController
    {
        // GET: ShoppingCart
        [System.Web.Mvc.HttpPost]
        public ActionResult AddShoppingCart([FromBody]ShoppingCartModel model)
        {
            var result = new JsonResult();
            try
            {
                model.AccountId = currentAccount.AccountId;
                var retValue = ShoppingCartBusiness.AddShoppingCart(model);
                if (retValue)
                {
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 2);
                }
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult GetMyShoppingCartList()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ShoppingCartBusiness.GetMyShoppingCartList(currentAccount.AccountId);
                double amount = 0;
                list.ForEach(x => amount += x.Amount);
                JsonResult.Add("shoppingcartList", list);
                JsonResult.Add("amoutTotal", amount);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        
        public ActionResult GetMyShoppingCartCount()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var count = ShoppingCartBusiness.GetMyShoppingCartCount(currentAccount.AccountId);
                JsonResult.Add("shoppingcartCount", count);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult UpdateShoppingCartQuantity(string cartId, int quantity)
        {
            var result = new JsonResult();
            try
            {
                var retValue = ShoppingCartBusiness.UpdateShoppingCartQuantity(cartId, quantity);
                if (retValue)
                {
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 2);
                }
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult UpdateShoppingCartEnabled()
        {
            var result = new JsonResult();
            try
            {
                var retValue = ShoppingCartBusiness.UpdateShoppingCartEnabled(currentAccount.AccountId);
                if (retValue)
                {
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 2);
                }
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        public ActionResult GetMyPreOrderList()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = ShoppingCartBusiness.GetMyPreOrderList(currentAccount.AccountId);                
                JsonResult.Add("preOrder", model);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        public ActionResult GetMyShoppingCartListByOrderId(string orderId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = ShoppingCartBusiness.GetMyShoppingCartListByOrderId(currentAccount.AccountId, orderId);
                JsonResult.Add("preOrder", model);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
    }
}