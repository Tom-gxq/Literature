using System;
using System.Collections.Generic;
using System.Text;

namespace wx.pay
{
    public class ManagePaymaxConfig
    {
        /**微信appid**/
        public static readonly string WX_APPID = "wx074713690982180a";
        /**微信商户号**/
        public static readonly string WX_MCHID = "";
        /**异步通知地址**/
        //public static readonly string WX_NOTIFYURL = "http://api.ejiajunxy.cn/WXNotify/WXPayManage_Notify";
        //Sandbox
        public static readonly string WX_NOTIFYURL = "http://s1api.ejiajunxy.cn/WXNotify/WXPayManage_Notify";
        /**微信支付方式**/
        public static readonly string WX_TRADETYPE = "APP";
        /**微信接口地址**/
        public static readonly string WX_URL = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        /**微信商户号**/
        public static readonly string WX_APPKEY = "3db525fbb02583dcd0fb48988737aaad";
        /**微信API密钥**/
        public static readonly string WX_APIKEY = "ZHOUYUXINGejiajunxiaoyuan2018030";
    }
}
