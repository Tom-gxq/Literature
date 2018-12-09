using AccountGRPCInterface;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Newtonsoft.Json.Linq;
using OrderGRPCInterface.Business;
using SP.Api.Cache;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.App_Start;
using WebApiGateway.App_Start.Crypt;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Account
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult GetAccountInfo()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = AccountBusiness.GetAccountFullInfo(currentAccount.AccountId);
                JsonResult.Add("accountInfo", list);
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
        public ActionResult UpdateAccountFullInfo([FromBody]AccountFullInfoModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                model.AccountId = currentAccount.AccountId;
                var retValue = AccountBusiness.UpdateAccountFullInfo(model);
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
        public ActionResult GetAlipayStr(string orderId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var payStr = GetAlipayParam(orderId);
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
        public ActionResult GetAlipayManageStr(string orderId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var payStr = GetAlipayManageParam(orderId);
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
        private string GetAlipayParam(string orderId)
        {
            var content = new JObject();
            //content.Add("timeout_express", "30m");
            StringBuilder body = new StringBuilder();
            try
            {
                var order = OrderBusiness.GetOrderByOrderId(orderId);
                if(order != null && order.orderStatus > 1)
                {
                    return string.Empty;
                }
                //content.Add("product_code", "QUICK_MSECURITY_PAY");
                content.Add("total_amount", order.amount);
                //content.Add("subject", "饿家军");
                content.Add("out_trade_no", order.orderCode);

                foreach (var item in order.productList)
                {
                    body.Append(!string.IsNullOrEmpty(item.productName) ? (item.productName + " ") : string.Empty);
                }
                //content.Add("body", body.ToString());
            }
            catch (Exception ex)
            {
            }


            //StringBuilder sb = new StringBuilder();
            //sb.Append("app_id=").Append("2016110202478860&");//获取密钥
            //sb.Append("biz_content=").Append(content).Append("&");
            //sb.Append("charset=utf-8&");//请求使用的编码格式
            //sb.Append("format=json&");//仅支持JSON
            //sb.Append("method=alipay.trade.pay&");//接口名称
            //sb.Append("notify_url=http://api.ejiajunxy.cn/AlipayNotify/PaymentNotify&");//支付宝服务器主动通知商户服务器里指定的页面http/https路径
            //sb.Append("sign_type=RSA2&");
            //sb.Append("timestamp=").Append(DateTime.Now.ToString("yyyy-MM-dd HH24:mm:ss")).Append("&");
            //sb.Append("version=1.0");
            //return sb.ToString();
            //IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", "2016110202478860", APP_PRIVATE_KEY, "json", "1.0", "RSA2", ALIPAY_PUBLIC_KEY, CHARSET, false);
            var client = Common.GetAlipayClient();
            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
            model.Body = !string.IsNullOrEmpty(body?.ToString())? body.ToString() : "饿家军餐饮";
            model.Subject = "饿家军";
            model.TotalAmount = content["total_amount"].Value<string>();
            model.ProductCode = "QUICK_MSECURITY_PAY";
            model.OutTradeNo = content["out_trade_no"].Value<string>();
            model.TimeoutExpress = "1m";//支付超时时间
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

        private string GetAlipayManageParam(string orderId)
        {
            var content = new JObject();
            StringBuilder body = new StringBuilder();
            try
            {
                var order = OrderBusiness.GetOrderByOrderId(orderId);
                if (order != null && order.orderStatus > 1)
                {
                    return string.Empty;
                }
                content.Add("total_amount", order.amount);
                content.Add("out_trade_no", order.orderCode);

                foreach (var item in order.productList)
                {
                    body.Append(!string.IsNullOrEmpty(item.productName) ? (item.productName + " ") : string.Empty);
                }
            }
            catch (Exception ex)
            {
            }

            var client = Common.GetAlipayManageClient();
            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
            model.Body = !string.IsNullOrEmpty(body?.ToString().Trim()) ? body.ToString().Trim() : "饿家军采购";
            model.Subject = "饿家军采购";
            model.TotalAmount = content["total_amount"].Value<string>();
            model.ProductCode = "QUICK_MSECURITY_PAY";
            model.OutTradeNo = content["out_trade_no"].Value<string>();
            model.TimeoutExpress = "1m";//支付超时时间
            //model.SellerId = "1725219681@qq.com";
            //model.SellerId = AlipayManageConfig.Partner;


            request.SetBizModel(model);
            request.SetNotifyUrl(AlipayManageConfig.Notify_Url);

            //这里和普通的接口调用不同，使用的是sdkExecute
            AlipayTradeAppPayResponse response = client.SdkExecute(request);
            //Http为了输出Utility.HtmlEncode是到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
            //Response.Write(HttpUtility.HtmlEncode(response.Body));
            //页面输出的response.Body就是orderString 可以直接给客户端请求，无需再做处理。
            var ret = response.Body;
            Common.WriteLog("alipayManageStr=" + HttpUtility.UrlDecode(ret));
            return ret;
        }


        private static string GetCurrentPath()
        {
            string basePath = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;
            return basePath + "/sign/";
        }

        public ActionResult SetAccountPayPwd([FromBody]PayPwdModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if(model.Password != model.ConfirmPassword)
            {
                JsonResult.Add("status", 2);
                result.Data = JsonResult;
                return result;
            }
            try
            {
                model.AccountId = currentAccount.AccountId;
                model.Password = StringCrypt.Encrypt(model.Password, ConfigInfo.ConfigInfoData.CryptKey.MessageKey);
                var retValue = AccountBusiness.SetAccountPayPwd(model);
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
        public ActionResult UpdateAccountPayPwd([FromBody]PayPwdModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            
            try
            {
                var accountModel = AccountBusiness.GetAccountFullInfo(currentAccount.AccountId);
                model.PrePassword = StringCrypt.Encrypt(model.PrePassword, ConfigInfo.ConfigInfoData.CryptKey.MessageKey);
                if (accountModel != null && accountModel.PayPassWord  != model.PrePassword)
                {
                    JsonResult.Add("status", 2);
                    result.Data = JsonResult;
                    return result;
                }
                model.AccountId = currentAccount.AccountId;
                model.Password = StringCrypt.Encrypt(model.Password, ConfigInfo.ConfigInfoData.CryptKey.MessageKey);
                var retValue = AccountBusiness.UpdateAccountPayPwd(model);
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

        public ActionResult UpdateAccountLoginPwd([FromBody]PayPwdModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var accountModel = AccountBusiness.GetAccountDetail(currentAccount.AccountId);
                model.PrePassword = StringCrypt.Encrypt(model.PrePassword, ConfigInfo.ConfigInfoData.CryptKey.MessageKey);
                if (accountModel != null && accountModel.Password != model.PrePassword)
                {
                    JsonResult.Add("status", 3);
                    result.Data = JsonResult;
                    return result;
                }
                if (model.Password != model.ConfirmPassword)
                {
                    JsonResult.Add("status", 4);
                    result.Data = JsonResult;
                    return result;
                }
                model.AccountId = currentAccount.AccountId;
                model.Password = StringCrypt.Encrypt(model.Password, ConfigInfo.ConfigInfoData.CryptKey.MessageKey);
                var retValue = AccountBusiness.UpdateAccountLoginPwd(model);
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
        public ActionResult IsExsitAccountPayPwd()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var accountModel = AccountBusiness.GetAccountFullInfo(currentAccount.AccountId);
               
                if (!string.IsNullOrEmpty(accountModel.PayPassWord))
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

        [System.Web.Http.HttpPost]
        public ActionResult UpdateAccountMobile([FromBody] RegisterAccount Params)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            Params.account = Common.CheckAccount(Params.account);

            if (string.IsNullOrEmpty(Params.account))
            {
                JsonResult.Add("status", 3);//手机号不合法
                result.Data = JsonResult;
                return result;
            }

            if (!string.IsNullOrEmpty(Params.code))
            {
                if (!Common.VerifyRegisterCode(Params.code, Params.account))
                {
                    JsonResult.Add("status", 4);//验证码错误
                    result.Data = JsonResult;
                    return result;
                }
            }
            else
            {
                JsonResult.Add("status", 5);//验证码不能为空
                result.Data = JsonResult;
                return result;
            }
            //账号已经存在
            var userAccount = AccountBusiness.GetAccount(Params.account);
            if (userAccount != null && !string.IsNullOrEmpty(userAccount.AccountId))
            {
                JsonResult.Add("status", 6);//账号已经存在
                result.Data = JsonResult;
                return result;
            }

            var model = new AccountModel();
            model.AccountId = currentAccount.AccountId;
            model.MobilePhone = Params.account;
            try
            {
                var retValue = AccountBusiness.UpdateAccountMobile(model);
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

        [System.Web.Http.HttpPost]
        public ActionResult BindOtherAccount([FromBody]BingAccountModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                
                model.AccountId = currentAccount.AccountId;
                var retValue = AccountBusiness.BindOtherAccount(model);
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

        public ActionResult IsBindedOtherAccount()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var accountModel = AccountBusiness.GetAccountDetail(currentAccount.AccountId);
                BindedModel model = new BindedModel();
                if (!string.IsNullOrEmpty(accountModel.AliBind))
                {
                    model.bindedAliPay = accountModel.AliBind;
                    model.IsBindedAliPay = true;
                }
                if (!string.IsNullOrEmpty(accountModel.WxBind))
                {
                    model.bindedWeiXin = accountModel.WxBind;
                    model.IsBindedWeiXin = true;
                }
                if (!string.IsNullOrEmpty(accountModel.QQBind))
                {
                    model.bindedQQ = accountModel.QQBind;
                    model.IsBindedQQ = true;
                }
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

        public ActionResult GetTradeListCount()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var total = AccountBusiness.GetTradeListCount(currentAccount.AccountId);
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

        public ActionResult GetTradeList(int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = AccountBusiness.GetTradeList(currentAccount.AccountId,pageIndex, pageSize);
                JsonResult.Add("tradeList", list);
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