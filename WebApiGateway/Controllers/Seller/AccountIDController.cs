using AccountGRPCInterface;
using SP.Api.Model.Account;
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
    public class AccountIDController : BaseController
    {
        public ActionResult UpdateAccountIDInfo([FromBody]AccountIDModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                model.AccountId = currentAccount.AccountId;
                var retValue = AccountBusiness.UpdateAccountIDInfo(model);
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