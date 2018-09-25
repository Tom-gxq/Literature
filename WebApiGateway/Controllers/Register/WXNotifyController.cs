using AccountGRPCInterface;
using OrderGRPCInterface.Business;
using SP.Api.Model.Account;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WebApiGateway.App_Start;
using WeddingCarService.WXPay;
using wx.pay;

namespace WebApiGateway.Controllers.Register
{
    public class WXNotifyController : Controller
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public string WXPay_Notify()
        {
            Common.WriteLog("--- WXPay_Notify --- Start");
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            string postStr = Encoding.GetEncoding("gbk").GetString(b);
            Common.WriteLog("postStr : " + postStr);
            //var postStr=@"<xml><appid><![CDATA[wx03a050a348a20ab5]]></appid><bank_type><![CDATA[CFT]]></bank_type>
            //<cash_fee><![CDATA[1]]></cash_fee>
            //<fee_type><![CDATA[CNY]]></fee_type>
            //<is_subscribe><![CDATA[N]]></is_subscribe>
            //<mch_id><![CDATA[1502431921]]></mch_id>
            //<nonce_str><![CDATA[698D51A19D8A121CE581499D7B701668]]></nonce_str>
            //<openid><![CDATA[oy8cI0t-siMyuMdP_6DT6RNQw7n4]]></openid>
            //<out_trade_no><![CDATA[20180530232401227952]]></out_trade_no>
            //<result_code><![CDATA[SUCCESS]]></result_code>
            //<return_code><![CDATA[SUCCESS]]></return_code>
            //<sign><![CDATA[9D6FA4D393FC7EB592ABF8DADE9E3CFA]]></sign>
            //<time_end><![CDATA[20180530230135]]></time_end>
            //<total_fee>1</total_fee>
            //<trade_type><![CDATA[APP]]></trade_type>
            //<transaction_id><![CDATA[4200000115201805304778541517]]></transaction_id>
            //</xml>";

            var dic = parseXmlToList(postStr);
            string appid = string.Empty;
            dic.TryGetValue("appid", out appid);
            string bank_type = string.Empty;
            dic.TryGetValue("bank_type", out bank_type);
            string cash_fee = string.Empty;
            dic.TryGetValue("cash_fee", out cash_fee);
            string fee_type = string.Empty;
            dic.TryGetValue("fee_type", out fee_type);
            string is_subscribe = string.Empty;
            dic.TryGetValue("is_subscribe", out is_subscribe);
            string mch_id = string.Empty;
            dic.TryGetValue("mch_id", out mch_id);
            string nonce_str = string.Empty;
            dic.TryGetValue("nonce_str", out nonce_str);
            string openid = string.Empty;
            dic.TryGetValue("openid", out openid);
            string out_trade_no = string.Empty;
            dic.TryGetValue("out_trade_no", out out_trade_no);
            string result_code = string.Empty;
            dic.TryGetValue("result_code", out result_code);
            string return_code = string.Empty;
            dic.TryGetValue("return_code", out return_code);
            string sign = string.Empty;
            dic.TryGetValue("sign", out sign);
            string time_end = string.Empty;
            dic.TryGetValue("time_end", out time_end);
            string total_fee = string.Empty;
            dic.TryGetValue("total_fee", out total_fee);
            string trade_type = string.Empty;
            dic.TryGetValue("trade_type", out trade_type);
            string transaction_id = string.Empty;
            dic.TryGetValue("transaction_id", out transaction_id);

            //设置package订单参数
            Hashtable parameters = new Hashtable();
            parameters.Add("appid", appid);
            parameters.Add("bank_type", bank_type);
            parameters.Add("cash_fee", cash_fee);
            parameters.Add("fee_type", fee_type);
            parameters.Add("is_subscribe", is_subscribe);
            parameters.Add("mch_id", mch_id);
            parameters.Add("nonce_str", nonce_str);
            parameters.Add("openid", openid);
            parameters.Add("out_trade_no", out_trade_no);
            parameters.Add("result_code", result_code);
            parameters.Add("return_code", return_code);
            parameters.Add("time_end", time_end);
            parameters.Add("total_fee", total_fee);
            parameters.Add("trade_type", trade_type);
            parameters.Add("transaction_id", transaction_id);
            string characterEncoding = "UTF-8";
            //获取签名
            string mySign = CreateMd5Sign("key", PaymaxConfig.WX_APIKEY, parameters, characterEncoding); ;

            Common.WriteLog("订单号是：" + out_trade_no);
            Common.WriteLog("我的签名是：" + mySign);
            Common.WriteLog("WeChat的签名是：" + sign);
            if (!"SUCCESS".Equals(result_code))
            {
                Common.WriteLog("微信返回的交易状态不正确（result_code=" + result_code + "）");
            }
            string result = "fail";
            if (sign.Equals(mySign))
            {
                Common.WriteLog("签名一致");
                if ("SUCCESS".Equals(result_code))
                {
                    bool retVal = false;
                    
                    if (out_trade_no.ToLower().StartsWith("gmhy"))
                    {
                        Common.WriteLog("HY State Update out_trade_no=" + out_trade_no);
                        retVal = UpdateHYState(out_trade_no);
                    }
                    else
                    {
                        retVal = UpdateOrderState(out_trade_no);

                    }
                    if (retVal)
                    {
                        result = "success";
                    }
                    else
                    {
                        result = "订单更新失败";
                    }
                }
            }
            else
            {
                Common.WriteLog("签名不一致");
            }
            Common.WriteLog("result:"+ result);
            Common.WriteLog("--- WXPay_Notify --- End");
            return result;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public string WXPayManage_Notify()
        {
            Common.WriteLog("--- WXPayManage_Notify --- Start");
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            string postStr = Encoding.GetEncoding("gbk").GetString(b);
            Common.WriteLog("postStr : " + postStr);
            
            var dic = parseXmlToList(postStr);
            string appid = string.Empty;
            dic.TryGetValue("appid", out appid);
            string bank_type = string.Empty;
            dic.TryGetValue("bank_type", out bank_type);
            string cash_fee = string.Empty;
            dic.TryGetValue("cash_fee", out cash_fee);
            string fee_type = string.Empty;
            dic.TryGetValue("fee_type", out fee_type);
            string is_subscribe = string.Empty;
            dic.TryGetValue("is_subscribe", out is_subscribe);
            string mch_id = string.Empty;
            dic.TryGetValue("mch_id", out mch_id);
            string nonce_str = string.Empty;
            dic.TryGetValue("nonce_str", out nonce_str);
            string openid = string.Empty;
            dic.TryGetValue("openid", out openid);
            string out_trade_no = string.Empty;
            dic.TryGetValue("out_trade_no", out out_trade_no);
            string result_code = string.Empty;
            dic.TryGetValue("result_code", out result_code);
            string return_code = string.Empty;
            dic.TryGetValue("return_code", out return_code);
            string sign = string.Empty;
            dic.TryGetValue("sign", out sign);
            string time_end = string.Empty;
            dic.TryGetValue("time_end", out time_end);
            string total_fee = string.Empty;
            dic.TryGetValue("total_fee", out total_fee);
            string trade_type = string.Empty;
            dic.TryGetValue("trade_type", out trade_type);
            string transaction_id = string.Empty;
            dic.TryGetValue("transaction_id", out transaction_id);

            //设置package订单参数
            Hashtable parameters = new Hashtable();
            parameters.Add("appid", appid);
            parameters.Add("bank_type", bank_type);
            parameters.Add("cash_fee", cash_fee);
            parameters.Add("fee_type", fee_type);
            parameters.Add("is_subscribe", is_subscribe);
            parameters.Add("mch_id", mch_id);
            parameters.Add("nonce_str", nonce_str);
            parameters.Add("openid", openid);
            parameters.Add("out_trade_no", out_trade_no);
            parameters.Add("result_code", result_code);
            parameters.Add("return_code", return_code);
            parameters.Add("time_end", time_end);
            parameters.Add("total_fee", total_fee);
            parameters.Add("trade_type", trade_type);
            parameters.Add("transaction_id", transaction_id);
            string characterEncoding = "UTF-8";
            //获取签名
            string mySign = CreateMd5Sign("key", ManagePaymaxConfig.WX_APIKEY, parameters, characterEncoding); ;

            Common.WriteLog("订单号是：" + out_trade_no);
            Common.WriteLog("我的签名是：" + mySign);
            Common.WriteLog("WeChat的签名是：" + sign);
            if (!"SUCCESS".Equals(result_code))
            {
                Common.WriteLog("微信返回的交易状态不正确（result_code=" + result_code + "）");
            }
            string result = "fail";
            if (sign.Equals(mySign))
            {
                Common.WriteLog("签名一致");
                if ("SUCCESS".Equals(result_code))
                {
                    bool retVal = false;
                    Common.WriteLog("订单号是：" + out_trade_no.ToLower()+"  result="+ out_trade_no.ToLower().StartsWith("gmhy"));
                    if (out_trade_no.ToLower().StartsWith("gmhy"))
                    {
                        Common.WriteLog("HY State Update out_trade_no=" + out_trade_no);
                        retVal = UpdateHYState(out_trade_no);
                    }
                    else
                    {
                        retVal = UpdateOrderState(out_trade_no);

                    }
                    if (retVal)
                    {
                        result = "success";
                    }
                    else
                    {
                        result = "订单更新失败";
                    }
                }
            }
            else
            {
                Common.WriteLog("签名不一致");
            }

            Common.WriteLog("--- WXPayManage_Notify --- End");
            return result;
        }
        /// <summary>
        /// 修改订单状态
        /// </summary>
        private bool UpdateOrderState(string orderCode)
        {
            bool retValue = false;
            string orderID = string.Empty;
            string orderPrice = string.Empty;
            if (!string.IsNullOrEmpty(orderCode))
            {
                try
                {
                    var order = OrderBusiness.GetOrderByOrderCode(orderCode);
                    if (order != null && (order.orderStatus == 1 || order.orderStatus == 4))
                    {
                        retValue = OrderBusiness.UpdateOrderStatusByOrderCode(orderCode, 2,1);//微信支付是1
                    }
                }
                catch (Exception ex)
                {
                    Common.WriteLog(ex.ToString());
                }
            }
            return retValue;
        }
        private static Dictionary<string,string> parseXmlToList(String xml)
        {
            Dictionary<string, string> retMap = new Dictionary<string, string>();
            try
            {
                XmlDocument xdoc = null;
                xdoc = new XmlDocument();
                xdoc.LoadXml(xml);
                XmlNode xn = xdoc.SelectSingleNode("xml");
                XmlNodeList xnl = xn.ChildNodes;
                if (xnl != null && xnl.Count != 0)
                {
                    foreach (XmlNode element in xnl)
                    {
                        retMap.Add(element.Name, element.InnerText);
                    }
                }
                xdoc.RemoveAll();
                xdoc = null;
            }
            catch (Exception e)
            {
                Common.WriteLog("Message=" + e.Message);
                Common.WriteLog("StackTrace=" + e.StackTrace);
            }
            return retMap;
        }

        /// <summary>
        /// 创建package签名
        /// </summary>
        /// <param name="key">密钥键</param>
        /// <param name="value">财付通商户密钥（自定义32位密钥）</param>
        /// <returns></returns>
        public virtual string CreateMd5Sign(string key, string value, Hashtable parameters, string _ContentEncoding)
        {
            var sb = new StringBuilder();
            //数组化键值对，并排序
            var akeys = new ArrayList(parameters.Keys);
            akeys.Sort();
            //循环拼接包参数
            foreach (string k in akeys)
            {
                var v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            //最后拼接商户自定义密钥
            sb.Append(key + "=" + value);
            //加密
            string sign = MD5Util.GetMD5(sb.ToString(), _ContentEncoding).ToUpper();
            //返回密文
            return sign;
        }

        private bool UpdateHYState(string orderCode)
        {
            bool retValue = false;
            Common.WriteLog("UpdateHYState orderCode=" + orderCode);
            if (!string.IsNullOrEmpty(orderCode))
            {
                try
                {
                    var order = AccountBusiness.GetAssociatorByCode(orderCode);
                    Common.WriteLog("HY State Update status=" + order?.status??string.Empty+ " associatorId="+ order?.associatorId??string.Empty);
                    if (order != null && (order.status == 0))
                    {
                        retValue = AccountBusiness.UpdateAssociatorStatus(new AssociatorModel()
                        {
                            associatorId = order.associatorId,
                            status = 2
                        });
                    }
                }
                catch (Exception ex)
                {
                    Common.WriteLog(ex.ToString());
                }
            }
            return retValue;
        }
    }
}