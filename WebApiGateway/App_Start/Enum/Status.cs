using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum VerifyCodeType
{
    //短信
    SMS = 0,

    //语音
    Voice = 1

}
public enum SendStatus
{
    /// <summary>
    /// 发送失败
    /// </summary>
    Failed = 0,

    /// <summary>
    /// 发送成功
    /// </summary>
    Success = 1,

    /// <summary>
    /// 余额不足
    /// </summary>
    NoBalance = 2,
}