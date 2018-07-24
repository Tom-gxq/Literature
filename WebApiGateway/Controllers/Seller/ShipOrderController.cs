using AccountGRPCInterface;
using OrderGRPCInterface.Business;
using SP.Api.Model.Order;
using SP.Api.Model.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Seller
{
    public class ShipOrderController : BaseController
    {
        public ActionResult ApplyPartner(int dormId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var retValue = AccountBusiness.ApplyPartner(currentAccount.AccountId, dormId);
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
        public ActionResult AddMyOrder([FromBody]OrderModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                model.accountId = currentAccount.AccountId;
                int status = 0;
                var orderId = OrderBusiness.AddMyOrder(model, out status);
                if (!string.IsNullOrEmpty(orderId))
                {
                    JsonResult.Add("status", 0);
                    JsonResult.Add("orderId", orderId);
                }
                else if (status == 10004)
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
    }
}