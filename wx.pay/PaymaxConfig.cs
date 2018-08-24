using System;
using System.Collections.Generic;
using System.Text;

namespace wx.pay
{
    public class PaymaxConfig
    {
        /**微信appid**/
        public static readonly string WX_APPID = "wx03a050a348a20ab5";
	    /**微信商户号**/
	    public static readonly string WX_MCHID = "1502431921";
        /**异步通知地址**/
        public static readonly string WX_NOTIFYURL = "http://api.ejiajunxy.cn/WXNotify/WXPay_Notify";
        //Sandbox
        //public static readonly string WX_NOTIFYURL = "http://s1api.ejiajunxy.cn/WXNotify/WXPay_Notify";
        /**微信支付方式**/
        public static readonly string WX_TRADETYPE = "APP";
	    /**微信接口地址**/
	    public static readonly string WX_URL = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        /**微信商户号**/
        public static readonly string WX_APPKEY = "430b257b8ae8fc6e34798a5ff2bb101e";
        /**微信API密钥**/
        public static readonly string WX_APIKEY = "ZHOUYUXINGejiajunxiaoyuan2018030";
    }
}
