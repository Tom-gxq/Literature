using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class RegisterAccount
    {
        /// <summary>
        /// 被邀请用户
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 验证码类型 0：短信，1：语音 默认短信
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 是否是验证码登陆
        /// </summary>
        public bool is_authorization { get; set; }
        /// <summary>
        /// 账号密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string full_name { get; set; }
    }
}
