using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sms.Service.Interfaces;
using Sms.Service.Utility;
using Sms.Service.Utility.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Sms.Service.Senders
{
    internal class Aliyun : ISendMessage
    {
        //private const string AccessKey = "LTAI7IQlrJo0CKNu";
        //private const string AccessSecret = "EBhjlPmQYGGV2apCW8fkotB1D6JZ2h";
        //private const string RegionId = "cn-hangzhou";
        //private const string Url = "http://sms.market.alicloudapi.com/singleSendSms";
        private const String product = "Dysmsapi";//短信API产品名称
        private const String domain = "dysmsapi.aliyuncs.com";//短信API产品域名
        private const String accessId = "LTAI7IQlrJo0CKNu";
        private const String accessSecret = "EBhjlPmQYGGV2apCW8fkotB1D6JZ2h";
        private const String regionIdForPop = "cn-hangzhou";

        private ProviderType providerType;

        public Aliyun(ProviderType type)
        {
            providerType = type;
        }

        public SendResult SendMobileMessage(string mobile, string message)
        {
            return new SendResult() { Code = Codes.NotSupport };
        }

        public SendResult SendMobileMessage(string mobile, Dictionary<string, string> templateDataDic, string templateId)
        {
            var code = Codes.Failed;
            var response = string.Empty;
            Exception ex = null;
            IClientProfile profile = DefaultProfile.GetProfile(regionIdForPop, accessId, accessSecret);
            DefaultProfile.AddEndpoint(regionIdForPop, regionIdForPop, product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //var sortedQueryString = GetSortedQueryString("【饿家军】", mobile, templateDataDic, templateId);

                //var signature = GetSignature(sortedQueryString);

                //response = RequestHelper.GetRequestServer(Url + "/?Signature=" + signature + "&" + sortedQueryString);

                //var returnJson = JsonConvert.DeserializeObject<JObject>(response);
                request.PhoneNumbers = mobile;
                request.SignName = "饿家军";
                request.TemplateCode = templateId;
                request.TemplateParam = "{\"code\":\"123\"}";
                //request.OutId = "xxxxxxxx";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                System.Console.WriteLine(sendSmsResponse.Message);
                if (sendSmsResponse.Code == "OK")
                {
                    //if (returnJson["Code"].ToString() == "OK")
                    //{
                    //    code = Codes.Success;
                    //}
                    code = Codes.Success;
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

        public SendResult SendVoiceMessage(string mobile, string message)
        {
            return new SendResult() { Code = Codes.NotSupport };
        }

        public SendResult SendVoiceMessage(string mobile, Dictionary<string, string> templateDataDic, string templateId)
        {
            return new SendResult() { Code = Codes.NotSupport };
        }

        #region 私有方法

        //private string GetSortedQueryString(string signName, string mobile, Dictionary<string, string> templateDataDic, string templateId)
        //{
        //    var dic = new Dictionary<string, string>();
        //    //阿里云分配的用户唯一ID  
        //    dic.Add("AccessKeyId", AccessKey);
        //    //格式化后的当前时间（时间与阿里云服务器时间相差15分钟会被拒绝请求） 
        //    dic.Add("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));           
        //    //固定参数  
        //    dic.Add("SignatureMethod", "HMAC-SHA1");
        //    dic.Add("Format", "JSON");
        //    dic.Add("SignatureVersion", "1.0");
        //    //用于请求的防重攻击，每次请求唯一  
        //    dic.Add("SignatureNonce", Guid.NewGuid().ToString());
        //    //API的命名，固定值  
        //    dic.Add("Action", "SendSms");
        //    //API的版本，固定值
        //    dic.Add("Version", "2017-05-25");
        //    //API支持的RegionID  
        //    dic.Add("RegionId", RegionId);
        //    //短信接收号码,支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式 
        //    dic.Add("PhoneNumbers", mobile);
        //    //短信签名(用户账号可新增签名，需审批)  
        //    dic.Add("SignName", signName);
            
        //    dic.Add("TemplateCode", templateId);

        //    var jObject = new JObject();
        //    foreach (var item in templateDataDic)
        //    {
        //        jObject.Add(item.Key, item.Value);
        //    }
        //    //短信模板变量替换JSON串,如果JSON中需要带换行符,参照标准的JSON协议。  如：{“code”:”1234”,”product”:”ytx”}    {\"customer\":\"nihao\"}  
        //    dic.Add("TemplateParam", JsonConvert.SerializeObject(jObject));

        //    var sortedDic = dic.OrderBy(item => item.Key).ToDictionary(o => o.Key, p => p.Value);

        //    var sortQuerys = new List<string>();
        //    foreach (var item in sortedDic)
        //    {
        //        sortQuerys.Add(SpecialUrlEncode(item.Key) + "=" + SpecialUrlEncode(item.Value));
        //    }
        //    return string.Join("&", sortQuerys);
        //}

        private string SpecialUrlEncode(string value)
        {
            return WebUtility.UrlEncode(value).Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="sortedQueryString"></param>
        /// <returns></returns>
        //private string GetSignature(string sortedQueryString)
        //{
        //    StringBuilder stringToSign = new StringBuilder();
        //    stringToSign.Append("GET").Append("&");
        //    stringToSign.Append(SpecialUrlEncode("/")).Append("&");
        //    stringToSign.Append(SpecialUrlEncode(sortedQueryString));
        //    var sign = Sign(AccessSecret + "&", stringToSign.ToString());
        //    return SpecialUrlEncode(sign);
        //}

        /// <summary>
        /// 签名加密
        /// </summary>
        /// <param name="accessSecret"></param>
        /// <param name="stringToSign"></param>
        /// <returns></returns>
        private string Sign(string accessSecret, string stringToSign)
        {
            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(accessSecret));
            var dataBuffer = Encoding.UTF8.GetBytes(stringToSign);
            var hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }

        #endregion
    }
}
