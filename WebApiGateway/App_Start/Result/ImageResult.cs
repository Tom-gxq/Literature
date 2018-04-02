using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiGateway.App_Start.Result
{
    public class ImageResult : ActionResult
    {
        MemoryStream Ms { get; set; }

        public ImageResult(MemoryStream ms)
        {
            Ms = ms;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = "image/Gif";
            response.ClearContent();
            response.BinaryWrite(Ms.ToArray());
        }
    }
}