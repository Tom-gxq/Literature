using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AccountGRPCInterface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WXApiGate.Common;
using WXApiGate.Model;

namespace WXApiGate.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        public string Index()
        {
            return "Hello MPServer";
        }
        // GET api/values
        [Route("GetWxValidate")]
        public JsonResult Get(string code, string encryptedData, string iv)
        {
            JsonResult result = new JsonResult(string.Empty);
            var _usrInfo = GetWxUserInfo(code,  encryptedData,  iv);
            if (_usrInfo != null)
            {
                var unionid = _usrInfo["unionId"].ToString();
                var account = AccountBusiness.GetAccountByUnionId(unionid);
                if(account != null)
                {
                    var accessTokenModel = new TokenModel();

                    TokenCache.Add(accessTokenModel, account.AccountId);

                    JObject ok = new JObject();
                    ok.Add("status",0);
                    ok.Add("access_token", accessTokenModel.Access_Token);
                    result.Value = ok;
                }
                else
                {
                    JObject ng = new JObject();
                    ng.Add("status", 1);
                    result.Value = ng;
                }
            }
            return result;
        }

        // GET api/values
        [Route("BindWxUnionId")]
        public JsonResult Add(string mobilePhone, string passWord, string code, string encryptedData, string iv)
        {
            var account = AccountBusiness.GetAccountByMobilePhone(mobilePhone);
            var _usrInfo = GetWxUserInfo(code, encryptedData, iv);
            if (_usrInfo != null)
            {
                var avatar = _usrInfo["avatarUrl"].ToString();
                var wxOpenId = _usrInfo["openId"].ToString();
                var nickName = _usrInfo["nickName"].ToString();
                var gender = _usrInfo.Value<int>("gender");
                var unionid = _usrInfo["unionId"].ToString();
                if (account != null &&!string.IsNullOrEmpty(account.AccountId))
                {
                    AccountBusiness.UpdateAccountWxUnionId(account.AccountId, unionid);
                }
                else
                {
                    AccountBusiness.CreateAccountWxUnionId(mobilePhone, avatar, wxOpenId, 1, nickName, passWord, unionid, gender);
                }
            }
            JObject ok = new JObject();
            ok.Add("status", 0);
            JsonResult result = new JsonResult(ok);
            return result;
        }
        
        [Route("GetSchoolDistrictList")]
        public ActionResult GetSchoolDistrictList(int dataId, long updateTime = 0)
        {
            var jobject = new JObject();
            try
            {
                var model = AddressBusiness.GetSchoolDistrictList(dataId, updateTime);
                //jobject.Add("regionList", list);
                jobject.Add("status", 0);
            }
            catch (Exception ex)
            {
                jobject.Add("error_msg", ex.Message);
                jobject.Add("status", 1);
            }
            JsonResult result = new JsonResult(jobject);
            return result;
        }
        [Route("GetDefaultSelectedAddress")]
        public ActionResult GetDefaultSelectedAddress()
        {
            var jobject = new JObject();
            try
            {
                var address = AddressBusiness.GetDefaultSelectedAddress(currentAccount.AccountId);                
                jobject.Add("defaultAddress", address);
                jobject.Add("status", 0);
            }
            catch (Exception ex)
            {
                jobject.Add("error_msg", ex.Message);
                jobject.Add("status", 1);
            }
            JsonResult result = new JsonResult(jobject);
            return result;
        }
        private JObject GetWxUserInfo(string code, string encryptedData, string iv)
        {
            StringBuilder urlStr = new StringBuilder();
            urlStr.AppendFormat(@"https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}"
                    + "&grant_type=authorization_code",
                    ConfigurationManager.AppSettings["XCXAppID"].ToString(),
                    ConfigurationManager.AppSettings["XCXAppSecrect"].ToString(),
                    code
                );
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlStr.ToString());
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            WxLoginModel vdModel = Newtonsoft.Json.JsonConvert.DeserializeObject<WxLoginModel>(retString);
            if (vdModel != null)
            {
                WxUsersHelper.AesIV = iv;
                WxUsersHelper.AesKey = vdModel.session_key;
                string resultData = new WxUsersHelper().AESDecrypt(encryptedData);
                JObject _usrInfo = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(resultData);
                return _usrInfo;
            }
            else
            {
                return null;
            }
        }
    }
}
