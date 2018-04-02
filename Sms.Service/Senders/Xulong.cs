using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sms.Service.Interfaces;
using Sms.Service.Utility;
using Sms.Service.Utility.ValueObjects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace Sms.Service.Senders
{
    internal class Xulong : ISendMessage
    {
        private const string Url = "http://a.cdxldx.com/sms.aspx?action=send";
        private const string Account = "ejjp";
        private const string Password = "123456";
        private const string UserId = "3020";

        private readonly ProviderType providerType;
        public Xulong(ProviderType type)
        {
            providerType = type;
        }

        public SendResult SendMobileMessage(string mobile, string message)
        {
            var code = Codes.Failed;
            var response = string.Empty;
            Exception ex = null;

            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHH24mmss");
                StringBuilder sb = new StringBuilder();               
                sb.Append("userid=").Append(HttpUtility.UrlEncode(UserId)).Append("&");
                sb.Append("account=").Append(HttpUtility.UrlEncode(Account)).Append("&");
                sb.Append("password=").Append(HttpUtility.UrlEncode(Password)).Append("&");
                //sb.Append("timestamp=").Append(HttpUtility.UrlEncode(timestamp)).Append("&");                
                //StringBuilder sortedQuery = new StringBuilder();
                //sortedQuery.Append(Account).Append(Password).Append(timestamp);
                //var sign = GetSignature(sortedQuery.ToString());
                //sb.Append("sign=").Append(HttpUtility.UrlEncode(sign)).Append("&");                
                sb.Append("mobile=").Append(HttpUtility.UrlEncode(mobile)).Append("&");
                sb.Append("content=").Append(HttpUtility.UrlEncode("【饿家军】" + message)).Append("&");
                sb.Append("sendTime=");
                //sb.Append("extno=");
                
                var requestResult = RequestHelper.PostRequestServer(Url, sb.ToString(), "application/x-www-form-urlencoded");
                
                if (!string.IsNullOrEmpty(requestResult))
                {
                    System.Console.WriteLine("mobile=" + mobile + "  requestResult=" + requestResult);
                    response = requestResult;
                    var doc = new XmlDocument();
                    doc.LoadXml(requestResult);
                    var returnstatus = doc.SelectNodes("/returnsms/returnstatus");
                    if (returnstatus != null&& returnstatus.Count > 0)
                    {
                        var mesg = doc.SelectNodes("/returnsms/message");
                        var responseCode = returnstatus.Item(0).InnerText;
                        if (responseCode.ToLower() == "success")
                        {
                            code = Codes.Success;
                        }
                        else if (mesg != null && mesg.Count > 0 && mesg.Item(0).InnerText == "对不起，您当前要发送的量大于您当前余额")
                        {
                            code = Codes.NoBalance;
                            WeixinHelper.SendNoBalanceMessage(providerType);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ex = e;
            }

            return new SendResult()
            {
                Code = code,
                Response = response,
                Ex = ex
            };
        }

        public SendResult SendMobileMessage(string mobile, Dictionary<string, string> templateDataDic, string templateId)
        {
            return new SendResult() { Code = Codes.NotSupport };
        }

        public SendResult SendVoiceMessage(string mobile, string message)
        {
            return new SendResult() { Code = Codes.NotSupport };
        }

        public SendResult SendVoiceMessage(string mobile, Dictionary<string, string> templateDataDic, string templateId)
        {
            return new SendResult() { Code = Codes.NotSupport };
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="sortedQueryString"></param>
        /// <returns></returns>
        private string GetSignature(string sortedQueryString)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(sortedQueryString);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString().ToLower();
        }
        
    }
}
