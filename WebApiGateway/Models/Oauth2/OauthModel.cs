using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApiGateway.Models.Oauth2
{
    public class ErrorModel
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public bool success { get; set; }
    }
    

    public enum ResultType
    {
        Error = 1,//错误
        Projects = 2,//多网络
        Success = 3,//成功
        Private = 4,//私有应用
        EGroup = 5,//外联用户
        NoPaid = 6,//未购买高级模式
        More = 7,//登录过于频繁
        ErrorCode = 8,//验证码错误
        NoProjectInstall = 9,//企业应用网络未安装
        NoAccountInstall = 10,//个人应用个人未安装
        NoProject = 11//企业应用用户没有加入的网络
    }
}