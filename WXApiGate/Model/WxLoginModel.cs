using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WXApiGate.Model
{
    public class WxLoginModel
    {
        /// <summary>
        /// session_key
        /// </summary>
        public string session_key { get; set; }
        /// <summary>
        /// 单平台用户唯一ID
        /// </summary>
        public string openid { get; set; }
    }
}
