using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiGateway.App_Start.Result
{
    public class XmlResult : ActionResult
    {
        // 可被序列化的内容
        object Data { get; set; }

        // Data的类型
        Type DataType { get; set; }
        

        string Format { get; set; }

        public XmlResult(object data, Type type, string format)
        {
            Data = data;
            DataType = type;
            Format = format;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = "application/json";

            if (Data != null)
            {
                response.Write(JsonConvert.SerializeObject(Data));
            }

        }
    }
}