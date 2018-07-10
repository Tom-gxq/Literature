using System;
using System.Collections.Generic;
using System.Text;

namespace wx.pay
{
    public class RandCharsUtils
    {
        public static string getRandomString(int length)
        { //length表示生成字符串的长度
            string baseStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            int number = 0;
            for (int i = 0; i < length; i++)
            {
                number = random.Next(baseStr.Length);
                sb.Append(baseStr.ToCharArray()[number]);
            }
            return sb.ToString();
        }

        /*
         * 订单开始交易的时间
         */
        public static string timeStart()
        {
            return string.Format("yyyyMMddHHmmss", DateTime.Now);
        }

        /*
         * 订单开始交易的时间
         */
        public static string timeExpire()
        {
            DateTime now = DateTime.Now;
            now.AddMinutes(30);
            return string.Format("yyyyMMddHHmmss", now);
        }
    }
}
