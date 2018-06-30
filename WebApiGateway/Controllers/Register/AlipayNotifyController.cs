using OrderGRPCInterface.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.App_Start;

namespace WebApiGateway.Controllers
{
    public class AlipayNotifyController : Controller
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public string Alipay_Notify()
        {
            Common.WriteLog("--- Alipay_Notify ---");           

            SortedDictionary<string, string> sPara = GetRequestPost(Request);
            string error = string.Empty;
            string result = string.Empty;
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                error = error + "verifyResult|" + verifyResult;
                double total_amount = 0;
                double buyer_pay_amount = 0;
                double receipt_amount = 0;
                if (Request.Form.AllKeys.Contains("total_amount") && !string.IsNullOrEmpty(Request.Form["total_amount"]))
                {
                    total_amount = double.Parse(Request.Form["total_amount"]);
                }
                if (Request.Form.AllKeys.Contains("buyer_pay_amount") && !string.IsNullOrEmpty(Request.Form["buyer_pay_amount"]))
                {
                    buyer_pay_amount = double.Parse(Request.Form["buyer_pay_amount"]);
                }
                if (Request.Form.AllKeys.Contains("receipt_amount") && !string.IsNullOrEmpty(Request.Form["receipt_amount"]))
                {
                    receipt_amount = double.Parse(Request.Form["receipt_amount"]);
                }
                if(total_amount != buyer_pay_amount || total_amount!= receipt_amount)
                {
                    result = "客户付款失败";
                }
                else if (verifyResult)//验证成功
                {
                    //商户订单号
                    string out_trade_no = Request.Form["out_trade_no"];
                    //支付宝交易号
                    string trade_no = Request.Form["trade_no"];
                    //交易状态
                    string trade_status = Request.Form["trade_status"];
                    string price = string.Empty;

                    error = error + DateTime.Now + "alipayNotify||||out_trade_no|" + out_trade_no + "trade_no|" + trade_no;


                    var retVal = UpdateOrderState(out_trade_no);
                    if (retVal)
                    {
                        result = "success";
                    }
                    else
                    {
                        result = "订单更新失败";
                    }
                }
                else//验证失败
                {
                    result = "fail";
                }
            }
            else
            {
                result = "无通知参数";
            }
            Common.WriteLog(error+ "   result="+ result);
            return result;
        }
        #region 支付宝通知
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns></returns>
        private SortedDictionary<string, string> GetRequestPost(HttpRequestBase request)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], request.Form[requestItem[i]]);
            }

            return sArray;
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
                    if (order != null && order.orderStatus == 1)
                    {
                        retValue = OrderBusiness.UpdateOrderStatusByOrderCode(orderCode, 2,2);
                    }                    
                }
                catch (Exception ex)
                {
                    Common.WriteLog(ex.ToString());
                }
            }
            return retValue;
        }
        #endregion
    }
}
