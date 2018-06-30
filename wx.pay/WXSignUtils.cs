using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace wx.pay
{
    /**
 * 微信支付签名
 * @author iYjrg_xiebin
 * @date 2015年11月25日下午4:47:07
 */
    public class WXSignUtils
    {
        //http://mch.weixin.qq.com/wiki/doc/api/index.php?chapter=4_3
        //商户Key：改成公司申请的即可
        //32位密码设置地址：  jdex1hvufnm1sdcb0e81t36k0d0f15nc 2irwoj0xjlt6a4vqecvq02hy5enzsrd5
        private static String Key = "公司微信商户的api密钥";

        /**
	     * 微信支付签名算法sign
	     * @param characterEncoding
	     * @param parameters
	     * @return
	     * @throws UnsupportedEncodingException 
	     */
    
        public static string createSign(string characterEncoding, Dictionary<object, object> parameters)
        {
            var sb = new StringBuilder();
            //所有参与传参的参数按照accsii排序（升序）
            var es = (from objDic in parameters orderby parameters.Keys ascending select objDic);
            
            foreach (var it in es)
            {
                var entry = it;
                string k = (string)entry.Key;
                object v = entry.Value;
                if (!string.IsNullOrEmpty(v?.ToString()) && "sign"!=k && "key"!= k)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            sb.Append("key=" + Key);
            
            //var sign = MD5Util.MD5Encode(sb.ToString(), characterEncoding).toUpperCase();
            return "";
        }
    }
}
