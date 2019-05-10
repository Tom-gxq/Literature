using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WXApiGate.Common;

namespace WXApiGate.Middleware
{
    public class AuthMiddleware
    {
        RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            JObject obj = new JObject();
            obj.Add("status", -1);
            var result = JsonConvert.SerializeObject(obj);
            var access_token = context.Request.Query["access_token"];
            try
            {
                //根据token获取授权用户信息
                var tokenModel = TokenCache.Get(access_token);

                if (!string.IsNullOrEmpty(tokenModel.Access_Token))
                {
                    DateTime expires = DateTime.MaxValue;
                    string accessTokenExpires = tokenModel.Access_Token_Expires;

                    if (!string.IsNullOrEmpty(accessTokenExpires))
                    {
                        expires = DateTime.Parse(accessTokenExpires);
                    }

                    if (expires >= DateTime.Now)
                    {
                        await _next(context);
                    }
                    else
                    {
                        context.Response.WriteAsync(result);
                    }
                }
                else
                {
                    context.Response.WriteAsync(result);
                }
            }
            catch(Exception ex)
            {
                context.Response.WriteAsync(result);
            }
        }
    }
}
