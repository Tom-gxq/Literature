using OrderGRPCInterface.Business;
using SmsGRPCInterface;
using SP.Api.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WebApiGateway.App_Start;
using WebApiGateway.Controllers.Base;
using WeddingCarService.WXPay;
using wx.pay;

namespace WebApiGateway.Controllers.Account
{
    public class WXPayController : BaseController
    {
        /// <summary>
        /// 微信支付主体接口
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWXPay(string orderId)
        {
            var order = OrderBusiness.GetOrderByOrderId(orderId);
            if (order != null && order.orderStatus > 1)
            {
                return Json(new { status= 1,msg = "订单已付", result = "" }, JsonRequestBehavior.AllowGet); ;
            }
            //************************************************支付参数接收********************************
            ///获取金额
            string detail = "饿家军";
            
            //根据appid和appappsecret获取refresh_token
            //string url_token = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appsecret);
            //string returnStr = tokenservice.GetToken(appId, appsecret);
            //时间戳
            var timeStamp = TenpayUtil.getTimestamp();
            //随机验证码
            var nonceStr = TenpayUtil.getNoncestr();

            //****************************************************************获取预支付订单编号***********************
            //设置package订单参数
            Hashtable packageParameter = new Hashtable();
            packageParameter.Add("appid", PaymaxConfig.WX_APPID);//开放账号ID  
            packageParameter.Add("mch_id", PaymaxConfig.WX_MCHID); //商户号
            packageParameter.Add("nonce_str", nonceStr); //随机字符串
            packageParameter.Add("body", detail); //商品描述    
            packageParameter.Add("out_trade_no", order.orderCode); //商家订单号 
            packageParameter.Add("total_fee", (order.amount * 100).ToString()); //商品金额,以分为单位    
            packageParameter.Add("spbill_create_ip", Request.UserHostAddress); //订单生成的机器IP，指用户浏览器端IP  
            packageParameter.Add("notify_url", PaymaxConfig.WX_NOTIFYURL); //接收财付通通知的URL  
            packageParameter.Add("trade_type", "APP");//交易类型  
            packageParameter.Add("fee_type", "CNY"); //币种，1人民币   66  
            //获取签名
            var sign = Common.CreateMd5Sign("key", PaymaxConfig.WX_APIKEY, packageParameter, Request.ContentEncoding.BodyName);
            //拼接上签名
            packageParameter.Add("sign", sign);
            //生成加密包的XML格式字符串
            string data = parseXML(packageParameter);
            //调用统一下单接口，获取预支付订单号码
            var prepayXml = SmsBusiness.SendHttp(PaymaxConfig.WX_URL, data);
            //string prepayXml = HttpUtil.Send(data, "https://api.mch.weixin.qq.com/pay/unifiedorder");

            //获取预支付ID
            var prepayId = string.Empty;
            if (string.IsNullOrEmpty(prepayXml))
            {
                var xdoc = new XmlDocument();
                xdoc.LoadXml(prepayXml);
                XmlNode xn = xdoc.SelectSingleNode("xml");
                XmlNodeList xnl = xn.ChildNodes;
                if (xnl.Count > 7)
                {
                    prepayId = xnl[7].InnerText;
                }
            }

            //**************************************************封装调起微信客户端支付界面字符串********************
            //设置待加密支付参数并加密
            Hashtable paySignReqHandler = new Hashtable();
            paySignReqHandler.Add("appid", PaymaxConfig.WX_APPID);
            paySignReqHandler.Add("partnerid", PaymaxConfig.WX_MCHID);
            paySignReqHandler.Add("prepayid", prepayId);
            paySignReqHandler.Add("package", "Sign=WXPay");
            paySignReqHandler.Add("noncestr", nonceStr);
            paySignReqHandler.Add("timestamp", timeStamp);
            var paySign = Common.CreateMd5Sign("key", PaymaxConfig.WX_APIKEY, paySignReqHandler, Request.ContentEncoding.BodyName);

            //设置支付包参数
            WxPayModel wxpaymodel = new WxPayModel();
            wxpaymodel.retcode = 0;//5+固定调起参数
            wxpaymodel.retmsg = "ok";//5+固定调起参数
            wxpaymodel.appid = PaymaxConfig.WX_APPID;//AppId,微信开放平台新建应用时产生
            wxpaymodel.partnerid = PaymaxConfig.WX_MCHID;//商户编号，微信开放平台申请微信支付时产生
            wxpaymodel.prepayid = prepayId;//由上面获取预支付流程获取
            wxpaymodel.package = "Sign=WXpay";//APP支付固定设置参数
            wxpaymodel.noncestr = nonceStr;//随机字符串，
            wxpaymodel.timestamp = timeStamp;//时间戳
            wxpaymodel.sign = paySign;//上面关键参数加密获得
            //将参数对象直接返回给客户端
            return Json(new { status = 0, msg = "", result = wxpaymodel }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWXPrePay(string orderId)
        {
            var order = OrderBusiness.GetOrderByOrderId(orderId);
            if (order != null && order.orderStatus > 1)
            {
                return Json(new { status = 1, msg = "订单已付", result = "" }, JsonRequestBehavior.AllowGet); ;
            }
            //************************************************支付参数接收********************************
            ///获取金额
            string detail = "饿家军";

            //根据appid和appappsecret获取refresh_token
            //string url_token = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, appsecret);
            //string returnStr = tokenservice.GetToken(appId, appsecret);
            //时间戳
            var timeStamp = TenpayUtil.getTimestamp();
            //随机验证码
            var nonceStr = TenpayUtil.getNoncestr().Replace("-", "");
            WxPrePayModel wxPrePaymodel = new WxPrePayModel();
            wxPrePaymodel.appid = PaymaxConfig.WX_APPID;
            wxPrePaymodel.mch_id = PaymaxConfig.WX_MCHID;
            wxPrePaymodel.nonce_str = nonceStr;
            wxPrePaymodel.body = detail;
            wxPrePaymodel.out_trade_no = order.orderCode;
            wxPrePaymodel.total_fee = (order.amount * 100).ToString();
            wxPrePaymodel.spbill_create_ip = Request.UserHostAddress;
            wxPrePaymodel.notify_url = PaymaxConfig.WX_NOTIFYURL;
            wxPrePaymodel.trade_type = "APP";
            wxPrePaymodel.fee_type = "CNY";
            //设置package订单参数
            Hashtable packageParameter = new Hashtable();
            packageParameter.Add("appid", PaymaxConfig.WX_APPID);//开放账号ID  
            packageParameter.Add("mch_id", PaymaxConfig.WX_MCHID); //商户号
            packageParameter.Add("nonce_str", nonceStr); //随机字符串
            packageParameter.Add("body", detail); //商品描述    
            packageParameter.Add("out_trade_no", order.orderCode); //商家订单号 
            packageParameter.Add("total_fee", (order.amount * 100).ToString()); //商品金额,以分为单位    
            packageParameter.Add("spbill_create_ip", Request.UserHostAddress); //订单生成的机器IP，指用户浏览器端IP  
            packageParameter.Add("notify_url", PaymaxConfig.WX_NOTIFYURL); //接收财付通通知的URL  
            packageParameter.Add("trade_type", "APP");//交易类型  
            packageParameter.Add("fee_type", "CNY"); //币种，1人民币   66  
            //获取签名
            var sign = Common.CreateMd5Sign("key", PaymaxConfig.WX_APIKEY, packageParameter, Request.ContentEncoding.BodyName);
            
            wxPrePaymodel.sign = sign;
            
            //将参数对象直接返回给客户端
            return Json(new { status = 0, msg = "", result = wxPrePaymodel }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 将类对象拼接成调起支付字符串
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        private string ReSetPayString(WxPayModel _model)
        {
            StringBuilder strpay = new StringBuilder();
            PropertyInfo[] props = _model.GetType().GetProperties();
            strpay.Append("{");
            foreach (PropertyInfo property in props)
            {
                strpay.Append(property.Name + ":\"" + property.GetValue(_model, null).ToString() + "\",");
            }
            strpay.Remove(strpay.Length - 1, 1);
            strpay.Append("}");
            return strpay.ToString();
        }

        /// <summary>
        /// 输出XML
        /// </summary>
        /// <returns></returns>
        public string parseXML(Hashtable _parameters)
        {
            var sb = new StringBuilder();
            sb.Append("<xml>");
            var akeys = new ArrayList(_parameters.Keys);
            foreach (string k in akeys)
            {
                var v = (string)_parameters[k];
                if (Regex.IsMatch(v, @"^[0-9.]$"))
                {
                    sb.Append("<" + k + ">" + v + "</" + k + ">");
                }
                else
                {
                    sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                }
            }
            sb.Append("</xml>");
            return sb.ToString();
        }

        

        

        /// <summary>
        /// 微信支付异步回调方法
        /// </summary>
        /// <returns></returns>
        
    }
}