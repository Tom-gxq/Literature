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
        public ActionResult GetSchoolLeadList(int orderStatus,int orderType=0)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = OrderBusiness.GetSchoolLeadList(currentAccount.AccountId, orderStatus, orderType);
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

        public ActionResult UpdateShipOrderStatus(string orderId, int orderStatus)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                if (orderStatus == 5)
                {
                    var retValue = OrderBusiness.UpdateShipOrderStatus(orderId, orderStatus, currentAccount.AccountId);
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

        [System.Web.Mvc.HttpPost]
        public ActionResult GetShipOrderList(int orderStatus, int orderType = 0, int pageIndex=1, int pageSize=10)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = OrderBusiness.GetShipOrderList(currentAccount.AccountId, orderStatus, orderType, pageIndex, pageSize);
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
        [System.Web.Mvc.HttpPost]
        public ActionResult UpdateShippingOrder(string shippingOrderId, int orderStatus)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                if (orderStatus == 5)
                {
                    var list = shippingOrderId.Split(',').ToList();
                    List<int> idList = new List<int>();
                    list.ForEach(x=>idList.Add(int.Parse(x)));
                    var retValue = OrderBusiness.UpdateShippingOrder(idList, orderStatus);
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

        public ActionResult GetPurchaseOrderList(int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                long total = 0;
                var list = OrderBusiness.GetPurchaseOrderList(currentAccount.AccountId, pageIndex, pageSize,out total);
                JsonResult.Add("orders", list);
                JsonResult.Add("status", 0);
                JsonResult.Add("total", total);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        public ActionResult GetPurchaseOrderByOrderId(string orderId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = OrderBusiness.GetPurchaseOrderByOrderId(orderId);
                JsonResult.Add("order", model );
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