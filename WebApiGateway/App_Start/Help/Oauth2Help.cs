using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using SP.Api.Cache;

public class Oauth2Help
{
    #region rediscode的操作
    /// <summary>
    /// 添加应用临时请求令牌code
    /// </summary>
    public static string SetAppAccountCode(string code, string accountid)
    {
        var codeParam = new CodeParam()
        {
            accountId = accountid
        };

        var flag = ApiTokenCache.SetAppAccountCode(code, JsonConvert.SerializeObject(codeParam));

        if (flag)
            return code;

        return string.Empty;
    }

    /// <summary>
    /// 获取应用临时请求令牌code
    /// </summary>
    public static CodeParam GetAppAccountCode(string code)
    {
        var codeParam = new CodeParam();
        string codeStr = ApiTokenCache.GetAppAccountCode(code);
        try
        {
            codeParam = JsonConvert.DeserializeObject<CodeParam>(codeStr);
            return codeParam;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// code存储的值的对象
    /// </summary>
    public class CodeParam
    {
        public string accountId { get; set; }
    }
    #endregion
}
