using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class AttributeValueController : BaseController
    {
        public JsonResult AddAttributeValue(AttributeValueDto attributeValue)
        {
            IAttributeValueAppService service = IocManager.Instance.Resolve<IAttributeValueAppService>();
            var result = service.AddAttributeValue(attributeValue);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UpdateAttributeValueDisplaySequence(int attributeId, int displaySequence)
        {
            IAttributeValueAppService service = IocManager.Instance.Resolve<IAttributeValueAppService>();
            var result = service.UpdateAttributeValueDisplaySequence(attributeId, displaySequence);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SetAttributeValue(int attributeId, string Value)
        {
            IAttributeValueAppService service = IocManager.Instance.Resolve<IAttributeValueAppService>();
            var result = service.SetAttributeValue(attributeId, Value);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteAttributeValue(int valueId)
        {
            IAttributeValueAppService service = IocManager.Instance.Resolve<IAttributeValueAppService>();
            var result = service.DeleteAttributeValue(valueId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAttributeValueList(int attributeId)
        {
            IAttributeValueAppService service = IocManager.Instance.Resolve<IAttributeValueAppService>();
            var result = service.GetAttributeValueList(attributeId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
