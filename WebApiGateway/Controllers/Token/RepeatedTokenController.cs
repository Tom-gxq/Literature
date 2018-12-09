using RepeatedTokenGRPCInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Token
{
    public class RepeatedTokenController : BaseController
    {
        public ActionResult GenerateRepeatedToken()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var ret = RepeatedTokenBusiness.GenerateRepeatedToken(currentAccount.AccountId);
                JsonResult.Add("model", ret);
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
        public ActionResult GetRepeatedTokenByKey(string key)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = RepeatedTokenBusiness.GetRepeatedTokenByKey(key, currentAccount.AccountId);
                JsonResult.Add("model", model);
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