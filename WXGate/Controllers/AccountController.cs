using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WXApiGateway.Common;

namespace WXGate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region +小程序模式注册登录
        /// <summary>
        /// 获取微信小程序授权信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("GetWxValidate")]
        public BaseGetResponse<WxResponseUserInfo> Get(string code, string encryptedData, string iv)
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
            WxValidateUserResponse vdModel = Newtonsoft.Json.JsonConvert.DeserializeObject<WxValidateUserResponse>(retString);
            if (vdModel != null)
            {
                GetUsersHelper.AesIV = iv;
                GetUsersHelper.AesKey = vdModel.session_key;
                string result = new GetUsersHelper().AESDecrypt(encryptedData);
                JObject _usrInfo = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                WxResponseUserInfo responseData = new WxResponseUserInfo
                {
                    nickName = _usrInfo["nickName"].ToString(),
                    gender = _usrInfo["gender"].ToString(),
                    city = _usrInfo["city"].ToString(),
                    province = _usrInfo["province"].ToString(),
                    country = _usrInfo["country"].ToString(),
                    avatarUrl = _usrInfo["avatarUrl"].ToString(),
                    sessionKey = vdModel.session_key
                };
                responseData.openId = _usrInfo["openId"].ToString();
                try
                {
                    responseData.unionId = _usrInfo["unionId"].ToString();
                }
                catch (Exception)
                {
                    responseData.unionId = "null";
                }
                return new BaseGetResponse<WxResponseUserInfo> { Code = ResultCode.NormalCode, Message = "微信认证成功", Data = responseData };
            }
            else
            {
                return new BaseGetResponse<WxResponseUserInfo> { Code = ResultCode.NotExistsValue, Message = "微信认证失败" };
            }
        }
        /// <summary>
        /// 小程序注册
        /// </summary>
        /// <param name="eneity"></param>
        /// <returns></returns>
        [Route("WXRegister")]
        public BaseUpdateModel PostWxRegister([FromBody]AccountDto eneity)
        {
            
        }
        /// <summary>
        /// 账号重复验证
        /// </summary>
        /// <param name="identifier">账号</param>
        /// <returns></returns>
        [Route("ExistsAccount")]
        public BaseGetResponse<int> GetExistsAccount(string identifier)
        {
            
        }
        #endregion
    }
}
