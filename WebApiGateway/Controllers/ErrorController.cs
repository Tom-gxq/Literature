using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace WebApiGateway.Controllers
{
    public class ErrorController : ApiController
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public JObject NotFound()
        {
            JObject resultObj = new JObject();
            resultObj.Add("error_code", ApiEnum.ErrorCode.NotFound.ToString());
            return resultObj;
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public JObject MethodNotAllowed()
        {
            JObject resultObj = new JObject();
            resultObj.Add("error_code", ApiEnum.ErrorCode.MethodNotAllowed.ToString());

            return resultObj;
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public JObject BaseNotNext(string error_code)
        {
            JObject resultObj = new JObject();
            resultObj.Add("error_code", error_code ?? ApiEnum.ErrorCode.ComBad.ToString());

            return resultObj;
        }
    }
}
