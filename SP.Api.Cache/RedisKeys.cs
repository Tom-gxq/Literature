using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Cache
{
    public class RedisKeys
    {
        //账号
        public const string AccountInfoKey = "H:A:I:"; //hash account info
        //API
        public const string ApiCodeKey = "H:Api:Code:"; //hash api code
        public const string ApiAuthKey = "H:Api:Auth:"; //hash api auth
        public const string ApiRequestKey = "H:Api:Request:"; //hash api request
        public const string ApiTokenKey = "H:Api:Token:"; //hash api token
        public const string ApiDeviceKey = "H:Api:Device:"; //hash api device
        public const string ApiVerifyKey = "H:Api:Verify:"; //hash api Verify
        public const string ApiAccessTokenKey = "H:Api:AccessToken:"; //hash api token        

        //短信限制
        public const string SendMobileMessageLimitKey = "H:S:M:M:L:"; //hash send mobile message limit

        //Session
        public const string SessionHashKey = "H:A:S:I:"; // session key
        public const string SessionAccountHashKey = "H:A:SA:l:"; // key accountid, field account,value sessionid

        //
        public const string LoginErrKey = "H:Api:Login:"; //hash api code


    }
}
