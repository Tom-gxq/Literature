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
                var orderId = OrderBusiness.AddMyOrder(model);
                if (!string.IsNullOrEmpty(orderId))
                {
                    JsonResult.Add("status", 0);
                    JsonResult.Add("orderId", orderId);
                }
                else if(orderId == null)
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

        public ActionResult GetMyOrderList(int orderStatus)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = OrderBusiness.GetMyOrderList(currentAccount.AccountId, orderStatus);
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

        public ActionResult GetSchoolLeadList(int orderStatus)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = OrderBusiness.GetSchoolLeadList(currentAccount.AccountId, orderStatus);
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

        public ActionResult GetSchoolLeadTradeList( int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            
            try
            {
                long total = 0;
                var list = OrderBusiness.GetSchoolLeadTradeList(currentAccount.AccountId, pageIndex,pageSize,out total);
                JsonResult.Add("tradeList", list);
                JsonResult.Add("total", total);
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
        public ActionResult GetSchoolLeadFinance()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = OrderBusiness.GetSchoolLeadFinance(currentAccount.AccountId);
                JsonResult.Add("finance", model);
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
        public ActionResult AddCashApply(string alipay, double money)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var retValue = OrderBusiness.AddCashApply(currentAccount.AccountId,alipay, money);
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
    }
}