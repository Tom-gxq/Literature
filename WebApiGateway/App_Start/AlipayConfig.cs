using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGateway.App_Start
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {
        #region 字段
        private static string partner = "";
        private static string private_key = "";
        private static string public_key = "";
        private static string input_charset = "";
        private static string sign_type = "";
        private static string notify_url = "http://api.ejiajunxy.cn/AlipayNotify/Alipay_Notify";
        #endregion

        static Config()
        {
            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            partner = "2088521158402583";

            //商户的私钥
            private_key = "MIIEowIBAAKCAQEA0dFmScMZ9bDFyO9mzwBf4aOsWz50532VYNTKTSAj3vGqGeTdfo" +
                          "To8PGsMpwXSSOyLn4fgauq6oU7FF10lUDK5l7MW1V01U9FEHHnisWFQstzisdQ /vBt0/" +
                          "pNg +dx80N0SwZEUQvr1j999eXfVNCKk9OeputZ6reWxym7VFelG8hRoCh38Aad6Eyx9u76u" +
                          "ZVj7SDGKElxLHJ5NscLFgEBKkuBtk5CM3PApnRP0 /0YGHFw1Q9eR48cLYNk+IYQ+NBrhLq7kpe" +
                          "2AU/95zCrUVAyl5/THhtXtgeXrR8DoNaQiSQeQnFkbH/AXUocQbD9xoSmpqxPpVZ5mgHi8vRFRSGI8w" +
                          "IDAQABAoIBAAbJh02LiiNwfHVriR /ZG0DpUoIBGzcB0Ps45vJnv8FOf/omdSPKmN5ycueQNRCgnvryCY" +
                          "LgUr8TYD3gaA0L84RQPLwXn5fm4I0PojXS /eSTQEGVAQG+NU9OIYPK0NzVqcVjhoI4qIBdvW1e6kKMHG97wP" +
                          "4VTutQ1QfMScsIqsajPTAiTTXvGG0ZQIIHa5KZQPCAV+aBVB8wcq+uXu+/t05y/bSaz/Z6jbaBu1py6vI7x4T9A/" +
                          "WpCQSPot0YnEUkjVHdPCLhiBUYyx7fS +Ghimx2w+HAqV+69CMlG+VJJZ2ZF2htS1OS+TE6ee9Hltf4uyzi5O2WhkvZ" +
                          "fB0OLNTsA0ECgYEA /1iIjd5sSd0i0Ny6c5yjRw0qH9jBeSxsX9iP7rcuQDIK7meF7bWtxt9bRhEQZOZQVtS2f+xlcnRC" +
                          "MyGom2lu474oyybx +9zQ0ASfzJxOoGEWl4iHxBg7IaltihgrwlAQfv6/FDmMNpyq4Arez/wH3jtwyGDR+vfFCwu42lgLzC0" +
                          "CgYEA0lsBzgZ02dD /qaRbdXsI3tYl795bNrriIRSrM8hIuXaGmC/Oim76mgMPJzQo6KwBArlHXYzLJqH9pDnyjAzd2hSCu" +
                          "VekZzqcIlU7hq +Fbb6alGWxJXDWntFPDgO2DDNvwdvDP6vpTVnHbUUgp5CTlnj9e53xMBkWNNGz8cwjPZ8CgYBQfuRtdNTGZEP5e9v" +
                          "7XkHKwEerSnWTcYGopWiaZHzcF+qCRXhe+4sQypDHgdGdPU3OUbhGk4tXjXbhD5dLhu1CNkw93sUiFPZu3UZTBmNzA6hPlObX1putfO/f" +
                          "PMdutja9EinCgnHFNZZYkpSzFEyWrj5brMoFR96CIOvhSShO8QKBgQCU23P +gJEmJoEVYkqaazOOxYpZIUf5BezJrh+s6DmWjOqYPZRyYDC" +
                          "U1j9d5cphRH7 +l3CKIf0M3KtqENu0AdLo5YTQddXQeyhMH3adTA4m6C/pfcELFru57bJ832FvZuLaE5MqtpPFehfnGnkOOwBGBl6SoOUxvT76p" +
                          "78oB+aUTwKBgEozzBnOBCcUYyrT0b5r5/2RU4n8672uztN3sDvDptXGWgCnRYDyExyvR+vE0D6twJKz46lIr/sAgj7TcgfHowFBCO2OHBBw6gx9JMd" + 
                          "KrnvkByVe8ksX2co3n6xpv6YqJHuPX3wP6HR82f3jLFeOHv2FxFu5PnhoyeTPPX3XlwyC";

            //支付宝的公钥，无需修改该值
            public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgVKas9ep+tOxGybBpVN4ALXc2DUM0ivZse/4S+mmMrF8yy/tvqUm9vB6sB1JVn2U/cGEaRDMmUPIr3KG9Noiw6roEndcVPullWMMuGxkGgS7OU23pvF61R7PJFAnTqviLBNHAdrVFU0WfQ6cTXgat2pEq858JOtIF+dqTOIaGMR4MBe8XbUYEBXJ/UyroDJkBWZCX6xnz7r2ReB+kgTp5jPMkofs+KMRnNhLxp+H6hvb324P42jrqDechcdBUeAjCo7+iMv85tS/1TcLMp4qt8QXvf5R6viNHdJPsqXLSM+HMxZxlroHMH5u62dFKVbf1DMVzLzWrVrRCpoLDCbPbwIDAQAB";

            input_charset = "utf-8";

            sign_type = "RSA2";
        }

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }
        public static string Notify_Url
        {
            get { return notify_url; }
        }
        #endregion
    }
}