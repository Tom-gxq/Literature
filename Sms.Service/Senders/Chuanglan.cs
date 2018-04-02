using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sms.Service.Interfaces;
using Sms.Service.Utility;
using Sms.Service.Utility.ValueObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sms.Service.Senders
{
    internal class Chuanglan : ISendMessage
    {
        private const string Url = "http://smssh1.253.com/msg/send/json";
        private const string Account = "N2612688";
        private const string Password = "chbLwky7v";

        private readonly ProviderType providerType;
        public Chuanglan(ProviderType type)
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
                var jObject = new JObject
                {
                    {"account", Account},
                    {"password", Password},
                    {"msg", "【饿家军】" + message},
                    {"phone", mobile.Replace("+", string.Empty)}
                };

                var content = JsonConvert.SerializeObject(jObject);

                var requestResult = RequestHelper.PostRequestServer(Url, content);
               
                if (!string.IsNullOrEmpty(requestResult))
                {
                    System.Console.WriteLine("mobile=" + mobile + "  requestResult="+ requestResult);
                    var returnJson = JsonConvert.DeserializeObject<JObject>(requestResult);
                    if (returnJson["code"] != null)
                    {
                        var responseCode = returnJson["code"].ToString();
                        if (responseCode == "0")
                        {
                            code = Codes.Success;
                        }
                        else if (responseCode == "110")
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
    }
}
