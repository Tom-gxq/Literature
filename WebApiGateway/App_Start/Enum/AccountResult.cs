using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGateway.App_Start.Enum
{
    public enum AccountResult
    {
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 0,

        /// <summary>
        /// 帐号验证成功（有网络）
        /// </summary>
        AccountSuccess = 1,

        /// <summary>
        /// 用户名错误
        /// </summary>
        AccountError = 2,

        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError = 3,

        /// <summary>
        /// 验证码输入错误
        /// </summary>
        VerifyCodeError = 4,

        /// <summary>
        /// 频繁登录错误，需要验证码
        /// </summary>
        AccountFrequentLoginError = 5,

        /// <summary>
        /// 多网络
        /// </summary>
        AccountMultiNetwork = 6,

        /// <summary>
        /// 账号不存在
        /// </summary>
        AccountNotExist = 7

    }
}