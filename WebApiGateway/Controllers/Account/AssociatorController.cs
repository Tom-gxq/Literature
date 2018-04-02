using AccountGRPCInterface;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Newtonsoft.Json.Linq;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.App_Start;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Account
{
    public class AssociatorController : BaseController
    {
        [System.Web.Mvc.HttpPost]
        public ActionResult BuyAssociator([FromBody]AssociatorModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                model.accountId = currentAccount.AccountId;
                var retValue = AccountBusiness.AddAssociator(model);
                if (!string.IsNullOrEmpty(retValue))
                {
                    JsonResult.Add("status", 0);
                    JsonResult.Add("associatorId", retValue);
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
        public ActionResult UpdateAssociatorStatus([FromBody]AssociatorModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                model.accountId = currentAccount.AccountId;
                var retValue = AccountBusiness.UpdateAssociatorStatus(model);
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

        public ActionResult GetAssociatorKindList()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = AccountBusiness.GetAssociatorKindList();
                JsonResult.Add("associatorList", list);
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
        public ActionResult BuyCoupons([FromBody]AssociatorModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                model.accountId = currentAccount.AccountId;
                var retValue = AccountBusiness.AddAssociator(model);
                if (!string.IsNullOrEmpty(retValue))
                {
                    JsonResult.Add("status", 0);
                    JsonResult.Add("associatorId", retValue);
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
        public ActionResult GetCouponsKindList()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = AccountBusiness.GetCouponsKindList();
                JsonResult.Add("couponsList", list);
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
        public ActionResult GetDiscountByAccountId(int kind)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = AccountBusiness.GetDiscountByAccountId(currentAccount.AccountId, kind);
                JsonResult.Add("discountList", list);
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
        public ActionResult GetAssociatorAlipayStr(string associatorId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var payStr = GetAlipayParam(associatorId);
                JsonResult.Add("payStr", payStr);
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
        private string GetAlipayParam(string associatorId)
        {
            var content = new JObject();
            try
            {
                var member = AccountBusiness.GetAssociatorById(associatorId);
                //content.Add("product_code", "QUICK_MSECURITY_PAY");
                content.Add("total_amount", member.amount);
                //content.Add("subject", "饿家军");
                content.Add("out_trade_no", member.payOrderCode);
                content.Add("body", "会员服务");
                
            }
            catch (Exception ex)
            {
            }
            var client = GetAlipayClient();
            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
            model.Body = content["body"].Value<string>();
            model.Subject = "饿家军";
            model.TotalAmount = content["total_amount"].Value<string>();
            model.ProductCode = "QUICK_MSECURITY_PAY";
            model.OutTradeNo = content["out_trade_no"].Value<string>();
            model.TimeoutExpress = "30m";
            model.SellerId = "1725219681@qq.com";
            request.SetBizModel(model);
            request.SetNotifyUrl(Config.Notify_Url);
            //这里和普通的接口调用不同，使用的是sdkExecute
            AlipayTradeAppPayResponse response = client.SdkExecute(request);
            //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
            //Response.Write(HttpUtility.HtmlEncode(response.Body));
            //页面输出的response.Body就是orderString 可以直接给客户端请求，无需再做处理。
            return response.Body;
        }
        private static IAopClient GetAlipayClient()
        {
            //支付宝网关地址
            // -----沙箱地址-----
            //string serverUrl = "http://openapi.alipaydev.com/gateway.do";
            // -----线上地址-----
            string serverUrl = "https://openapi.alipay.com/gateway.do";
            //应用ID
            string appId = "2016110202478860";
            //商户私钥
            string privateKeyPem = "MIIEowIBAAKCAQEA0dFmScMZ9bDFyO9mzwBf4aOsWz50532VYNTKTSAj3vGqGeTdfoTo8PGsMpwXSSOyLn4fgauq6oU7FF10lUDK5l7MW1V01U9FEHHnisWFQstzisdQ/vBt0/pNg+dx80N0SwZEUQvr1j999eXfVNCKk9OeputZ6reWxym7VFelG8hRoCh38Aad6Eyx9u76uZVj7SDGKElxLHJ5NscLFgEBKkuBtk5CM3PApnRP0/0YGHFw1Q9eR48cLYNk+IYQ+NBrhLq7kpe2AU/95zCrUVAyl5/THhtXtgeXrR8DoNaQiSQeQnFkbH/AXUocQbD9xoSmpqxPpVZ5mgHi8vRFRSGI8wIDAQABAoIBAAbJh02LiiNwfHVriR/ZG0DpUoIBGzcB0Ps45vJnv8FOf/omdSPKmN5ycueQNRCgnvryCYLgUr8TYD3gaA0L84RQPLwXn5fm4I0PojXS/eSTQEGVAQG+NU9OIYPK0NzVqcVjhoI4qIBdvW1e6kKMHG97wP4VTutQ1QfMScsIqsajPTAiTTXvGG0ZQIIHa5KZQPCAV+aBVB8wcq+uXu+/t05y/bSaz/Z6jbaBu1py6vI7x4T9A/WpCQSPot0YnEUkjVHdPCLhiBUYyx7fS+Ghimx2w+HAqV+69CMlG+VJJZ2ZF2htS1OS+TE6ee9Hltf4uyzi5O2WhkvZfB0OLNTsA0ECgYEA/1iIjd5sSd0i0Ny6c5yjRw0qH9jBeSxsX9iP7rcuQDIK7meF7bWtxt9bRhEQZOZQVtS2f+xlcnRCMyGom2lu474oyybx+9zQ0ASfzJxOoGEWl4iHxBg7IaltihgrwlAQfv6/FDmMNpyq4Arez/wH3jtwyGDR+vfFCwu42lgLzC0CgYEA0lsBzgZ02dD/qaRbdXsI3tYl795bNrriIRSrM8hIuXaGmC/Oim76mgMPJzQo6KwBArlHXYzLJqH9pDnyjAzd2hSCuVekZzqcIlU7hq+Fbb6alGWxJXDWntFPDgO2DDNvwdvDP6vpTVnHbUUgp5CTlnj9e53xMBkWNNGz8cwjPZ8CgYBQfuRtdNTGZEP5e9v7XkHKwEerSnWTcYGopWiaZHzcF+qCRXhe+4sQypDHgdGdPU3OUbhGk4tXjXbhD5dLhu1CNkw93sUiFPZu3UZTBmNzA6hPlObX1putfO/fPMdutja9EinCgnHFNZZYkpSzFEyWrj5brMoFR96CIOvhSShO8QKBgQCU23P+gJEmJoEVYkqaazOOxYpZIUf5BezJrh+s6DmWjOqYPZRyYDCU1j9d5cphRH7+l3CKIf0M3KtqENu0AdLo5YTQddXQeyhMH3adTA4m6C/pfcELFru57bJ832FvZuLaE5MqtpPFehfnGnkOOwBGBl6SoOUxvT76p78oB+aUTwKBgEozzBnOBCcUYyrT0b5r5/2RU4n8672uztN3sDvDptXGWgCnRYDyExyvR+vE0D6twJKz46lIr/sAgj7TcgfHowFBCO2OHBBw6gx9JMdKrnvkByVe8ksX2co3n6xpv6YqJHuPX3wP6HR82f3jLFeOHv2FxFu5PnhoyeTPPX3XlwyC";
            string publicKeyPem = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0dFmScMZ9bDFyO9mzwBf4aOsWz50532VYNTKTSAj3vGqGeTdfoTo8PGsMpwXSSOyLn4fgauq6oU7FF10lUDK5l7MW1V01U9FEHHnisWFQstzisdQ/vBt0/pNg+dx80N0SwZEUQvr1j999eXfVNCKk9OeputZ6reWxym7VFelG8hRoCh38Aad6Eyx9u76uZVj7SDGKElxLHJ5NscLFgEBKkuBtk5CM3PApnRP0/0YGHFw1Q9eR48cLYNk+IYQ+NBrhLq7kpe2AU/95zCrUVAyl5/THhtXtgeXrR8DoNaQiSQeQnFkbH/AXUocQbD9xoSmpqxPpVZ5mgHi8vRFRSGI8wIDAQAB";

            IAopClient client = new DefaultAopClient(serverUrl, appId, privateKeyPem, "json", "1.0", "RSA2", publicKeyPem, "utf-8", false);


            return client;
        }
    }
}