using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Enum
{
    /// <summary>
    /// 认证类型
    /// </summary>
    [Serializable]
    public enum AuthType
    {
        /// <summary>
        /// 注册认证
        /// </summary>
        Register = 0,

        /// <summary>
        /// 电子邮件修改认证
        /// </summary>
        ChangeEmail = 1,

        /// <summary>
        /// 忘记密码，重置认证
        /// </summary>
        ForgetPassword = 2,
        
        /// <summary>
        /// 手机号码注册
        /// </summary>
        MobilePhoneRegister = 6,

        /// <summary>
        /// 手机号修改
        /// </summary>
        MobilePhoneModify = 7,

        /// <summary>
        /// 通过手机号验证找回密码
        /// </summary>
        MobilePhoneForgetPassword = 8

        

    }

    /// <summary>
    /// 认证状态
    /// </summary>
    [Serializable]
    public enum AuthStatus
    {
        /// <summary>
        /// 未使用
        /// </summary>
        Unused = 0,
        /// <summary>
        /// 已经使用,同意邀请
        /// </summary>
        Agree = 1,
        /// <summary>
        /// 邀请码失效
        /// </summary>
        Failure = 2,

        /// <summary>
        /// 已经使用，并且拒绝邀请加入
        /// </summary>
        Refused = 3,
    }
}
