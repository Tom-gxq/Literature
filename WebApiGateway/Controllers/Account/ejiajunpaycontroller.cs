using AccountGRPCInterface;
using OrderGRPCInterface.Business;
using SP.Api.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiGateway.App_Start.Crypt;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Account
{
    public class EjiajunPayController : BaseController
    {
        public ActionResult BalancePay(string token, string password,double amount,string orderCode, string sign)
        {
            var result = new JsonResult();
            var validateSign = StringMD5.GetMd5Str32($"{token}{currentAccount.AccountId}{password}{amount}{orderCode}");
            sign = validateSign;
            if (validateSign.ToLower() != sign.ToLower())
            {
                JsonResult.Add("status", 3);
            }
            else
            {
                var model = OrderBusiness.GetSchoolLeadFinance(currentAccount.AccountId);
                if (model == null || (model.haveAmount - model.useAmount - model.activeAmount - model.applyAmount - model.consumeAmount) < amount)
                {
                    JsonResult.Add("status", 1);
                }
                else
                {
                    var ret = AccountBusiness.BalancePay(currentAccount.AccountId, token, password, amount, orderCode, sign);
                    if (ret)
                    {
                        JsonResult.Add("status", 0);
                    }
                    else
                    {
                        JsonResult.Add("status", 2);
                    }
                }
            }
            return result;
        }
    }
}