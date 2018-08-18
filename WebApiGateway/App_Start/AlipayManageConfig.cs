using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGateway.App_Start
{
    public class AlipayManageConfig
    {
        #region 字段
        private static string partner = "";
        private static string private_key = "";
        private static string public_key = "";
        private static string input_charset = "";
        private static string sign_type = "";
        //private static string notify_url = "http://api.ejiajunxy.cn/AlipayNotify/AlipayManage_Notify";
        //Sandbox
        private static string notify_url = "http://s1api.ejiajunxy.cn/AlipayNotify/AlipayManage_Notify";
        #endregion

        static AlipayManageConfig()
        {
            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            partner = "2088521158402583";

            //商户的私钥
            private_key = "MIIEogIBAAKCAQEA0Lwqcl56YhrLM4xvYsVUG2sSisPM67YwjqFjlyrRfh4QYr6A" +
                            "8ftWZggw4EEyC/7QlZct26L//iQx0xJCJdeZX1Prp+AO5BP6A/LTihrHRSImnKk6" +
                            "bshNK8ftGRVV+gn5xpAcGSVXNmMTau/13YT5oImyAN2N57+Vfw5khZjrDQSdnI/v" +
                            "1ZOHMh+6M1CpooGnXFUQhfvuOUz8tnG3rhPRUeJx+OI/WokSwDvssceCw7J1tq1y" +
                            "FajsT3veBAH4Lf0CkxQ14cdOhzw1AKyB7QqRpUPVS7s/8m5Dj7BpmOhlNdaC8Qxo" +
                            "mStQ47/AMfgf0ckVPxC4y4TIFLLEE0r/P+qqWQIDAQABAoIBABhxEB9YRMhcDtM4" +
                            "bIqKRLD5NrCdAM/RA5qP91NqJiG4b37Ag+TR42oLJ3365FtXOSFrFGmVipg9sL/g" +
                            "cXJw7nqlHwEHUXcPh8USmAah4BeOSisp/befKLoprO+0+d5PcLn5gfDH2JB7xXhO" +
                            "JOINghV0Si2jw8wROPYpv0eNgmiBnZZkgOeWMSa0+7A0orAESXR/wCNyDqM+6tpU" +
                            "5VxSSC88G36Uisl+T2rygYqpUvxUJKiIw6ybPLK+AtjnZFnjSMfULs2WswN8EAcd" +
                            "WXQSV2VDYt9o89LUPkNkaZvrLvBAW0uhTPpcGY3ojcZ076J3mc6d90mKhaV/U4M6" +
                            "fLTPzMkCgYEA99xlzm7i5V+RY9hYElSpbqQqL67fdETdiXu/D1sAaU5gzoBjyCwO" +
                            "ZTVWWVYALAu4rg/yKya1He0W09P5Je9diwOepjfeAhEAdEn6mIX95yXHvEKr+KeS" +
                            "gOHPLBrpD8vjz57Qx24nqPhv9cqN+bmoqciHaPDN6UYxl9q3Cx5f1Y8CgYEA15bc" +
                            "06u1E+GyWi91qLIoPCmdrhgWvFz/oEoeSQJsudO527ESAHzWUbIQMU9F+5elahbF" +
                            "i3l/NHb8tI6RJxF3adiNBm5IBO7kTPvnM3rOGf+PCpj6WrcB4x3gznYIC5M0Kq58" +
                            "MWzlgHz0h6bGFZFhU/iDHDr+9CfxKO7rn9JlnZcCgYBhICNgtkEBIublGjTA0h4m" +
                            "tfu1/a+gbw/kvPIMgVty6Hy0zsjK+sCVVkZE8ZdVqy4uPx9lW2CjnnFQhoLeI6Mj" +
                            "Q0La3Y+IfcMQzqB9PhxVhI91LScYZAnbqSC2psDnKL1XcWNcksTFyLyOs1XZIrB9" +
                            "/49YRuxZWeE7IUCTAc8J7wKBgGOP/meassN9xdo0dMf1jvNJ3NIbudQ/tqYV1Kqu" +
                            "/ftLtlepDMFA5dFQxH2hOJaaUAAQiCaDc6WNFeNku9ApFtbA9U/0+jmdAb4zz3BM" +
                            "1IXZKfwg7e2a+roigEGY7No7nyne7uf8fHi8PEmxUWWAgExBRntxG3EySQM4Y4Sp" +
                            "q9f9AoGAMqLW2Wlu/odEOivSnwvUkTL0zfyEQz31L7gavgQv6wkgfA9O4bpJtIEe" +
                            "gQ8+dsdr9ez45yEXiuPc++xhs6RzivwejhguhsmAqclYV3w4Wr5kwjzL+5NYIWUe" +
                            "+t4DMzqBHDC2USq/wzcGwTJdE/i8e7LUNtjDB61L3Lr8FcxAOMQ=";

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