using OrderGRPCInterface.Business;
using SP.Api.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Order
{
    public class OrderController : BaseController
    {
        [System.Web.Mvc.HttpPost]
        public ActionResult AddMyOrder([FromBody]OrderModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                model.accountId = currentAccount.AccountId;
                int status = 0;
                var orderId = OrderBusiness.AddMyOrder(model,out status);
                if (!string.IsNullOrEmpty(orderId))
                {
                    JsonResult.Add("status", 0);
                    JsonResult.Add("orderId", orderId);
                }
                else if(status == 10004)
                {
                    JsonResult.Add("status", 3);
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
        public ActionResult UpdateOrderStatus(string orderId,int orderStatus)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                if (orderStatus == 5)
                {
                    var retValue = OrderBusiness.UpdateOrderStatus(orderId, orderStatus);
                    if (retValue)
                    {
                        JsonResult.Add("status", 0);
                    }
                    else
                    {
                        JsonResult.Add("status", 2);
                    }
                }
                else
                {
                    JsonResult.Add("status", 0);
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

        public ActionResult GetMyOrderList(string orderDate)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = OrderBusiness.GetMyOrderList(currentAccount.AccountId, orderDate);
                JsonResult.Add("orderList", list);
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

        public ActionResult SearchOrderKeywordList(string keyWord, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = OrderBusiness.SearchOrderKeywordList(currentAccount.AccountId, keyWord, pageIndex, pageSize);
                JsonResult.Add("orderList", list);
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