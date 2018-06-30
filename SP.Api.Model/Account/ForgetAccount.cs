using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class ForgetAccount
    {
        /// <summary>
        /// 被邀请用户
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 账号密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 账号密码
        /// </summary>
        public string confirmPassword { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
    }
}
