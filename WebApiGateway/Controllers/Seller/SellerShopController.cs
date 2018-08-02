using ProductGRPCInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Seller
{
    public class SellerShopController : BaseController
    {
        public ActionResult UpdateOpenShopStatus(int shopId,bool status)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var ret = ProductBusiness.UpdateOpenShopStatus(shopId, status);
                if (ret)
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
        public ActionResult GetShopStatus()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var ret = ProductBusiness.GetShopStatus(currentAccount.AccountId);
                if (ret)
                {
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 2);
                }
                JsonResult.Add("shopStatus", ret);
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